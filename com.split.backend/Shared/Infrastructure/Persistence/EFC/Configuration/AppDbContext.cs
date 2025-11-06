using com.split.backend.Households.Infrastructure.Persistence.EFC.Configuration.Extensions;
using com.split.backend.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        //ApplyContexts
        //IAM Context
        builder.ApplyIamConfiguration();
        builder.ApplyHouseHoldConfiguration();
        
        
        //General Naming Convention for the db objects
        builder.UseSnakeCaseNamingConvention();
    }
    
}