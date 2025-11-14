using com.split.backend.HouseholdMembers.Application.ACL;
using com.split.backend.HouseholdMembers.Application.Internal.CommandServices;
using com.split.backend.HouseholdMembers.Application.Internal.QueryServices;
using com.split.backend.HouseholdMembers.Domain.Repositories;
using com.split.backend.HouseholdMembers.Domain.Services;
using com.split.backend.HouseholdMembers.Infrastructure.Persistence.EFC.Repositories;
using com.split.backend.HouseholdMembers.Interface.ACL;
using com.split.backend.Households.Domain.Repositories;
using com.split.backend.Households.Infrastructure.Persistence.EFC.Repositories;
using com.split.backend.IAM.Application.Internal.CommandServices;
using com.split.backend.IAM.Application.Internal.OutboundServices;
using com.split.backend.IAM.Application.Internal.QueryServices;
using com.split.backend.IAM.Domain.Repositories;
using com.split.backend.IAM.Domain.Services;
using com.split.backend.IAM.Infrastructure.Hashing.BCrypt.Services;
using com.split.backend.IAM.Infrastructure.Persistence.EFC.Repositories;
using com.split.backend.IAM.Infrastructure.Pipeline.MiddleWare.Extensions;
using com.split.backend.IAM.Infrastructure.Tokens.JWT.Configuration;
using com.split.backend.IAM.Infrastructure.Tokens.JWT.Services;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using com.split.backend.Shared.Interfaces.ASP.Configuration;
using Cortex.Mediator.Behaviors;
using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using IUnitOfWork = com.split.backend.Shared.Domain.Repositories.IUnitOfWork;
using UnitOfWork = com.split.backend.Shared.Infrastructure.Persistence.EFC.Repositories.UnitOfWork;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));


// Add Cors Policy

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if(connectionString == null) throw new InvalidOperationException("Connection string not found.");


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information);
});

//Learn more about configuring Swagger/OpenApi at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "Com.Harmonix.API",
            Version = "v1",
            Description = "Harmonix API",
            TermsOfService = new Uri("https://github.com/harmonix"),
            Contact = new OpenApiContact
            {
                Name = "Harmonix S.A.C.",
                Email = "harmonix@gmail.com"
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
    options.EnableAnnotations();
});

//Dependency Injection

//Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// IAM Bounded Context Injection Configuration
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();

// Households Bounded Context Injection Configuration
builder.Services.AddScoped<IHouseHoldRepository, HouseHoldRepository>();

// HouseholdMembers Bounded Context Injection Configuration
builder.Services.AddScoped<IHouseholdMemberRepository, HouseholdMemberRepository>();
builder.Services.AddScoped<IHouseholdMemberCommandService, HouseholdMemberCommandService>();
builder.Services.AddScoped<IHouseholdMemberQueryService, HouseholdMemberQueryService>();

// ACL Facades for HouseholdMembers
builder.Services.AddScoped<IHouseholdContextFacade, HouseholdContextFacade>();
builder.Services.AddScoped<IUserContextFacade, UserContextFacade>();

// TokenSettings Configuration
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();

// JWT Authentication Configuration
var tokenSettings = builder.Configuration.GetSection("TokenSettings").Get<TokenSettings>();
if (tokenSettings?.Secret != null)
{
    var key = Encoding.ASCII.GetBytes(tokenSettings.Secret);
    
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });
}


//Mediator Configuration

//Add Mediator Injection Configuration
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));

// Add Cortex Mediator for Event Handling
builder.Services.AddCortexMediator(
    configuration: builder.Configuration,
    handlerAssemblyMarkerTypes: [typeof(Program)], configure: options =>
    {
        options.AddOpenCommandPipelineBehavior(typeof(LoggingCommandBehavior<>));
    });

var app = builder.Build();

// Verify if the database exists and create it if it doesnt 
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();

    if (app.Environment.IsDevelopment())
        context.Database.EnsureDeleted();
    
    context.Database.EnsureCreated();
}

//Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Apply CORS Policy
app.UseCors("AllowAllPolicy");

//Add Authorization Middleware to Pipeline
app.UseRouting();

app.UseAuthentication();
app.UseRequestAuthorization();

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();