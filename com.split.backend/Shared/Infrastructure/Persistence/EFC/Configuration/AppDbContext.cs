using com.split.backend.HouseholdMembers.Infrastructure.Persistence.EFC.Configuration.Extensions;
﻿using com.split.backend.Bills.Infrastructure.Persistence.EFC.Configuration.Extensions;
using com.split.backend.Households.Infrastructure.Persistence.EFC.Configuration.Extensions;
using com.split.backend.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;
using com.split.backend.Settings.Domain.Models.Aggregates;
using com.split.backend.Settings.Infrastructure.Persistence.EFC.Configuration.Extensions;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Setting> Settings => Set<Setting>();

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
        builder.ApplySettingsConfiguration();
      
        //Household-Member
        builder.ApplyHouseholdMemberConfiguration();
        
        //Bills
        builder.ApplyBillsConfiguration();
        
        
        //General Naming Convention for the db objects
        builder.UseSnakeCaseNamingConvention();
        
        // Settings column names must stay camelCase for frontend compatibility
        builder.Entity<Setting>().Property(setting => setting.UserId).HasColumnName("userId");
        builder.Entity<Setting>().Property(setting => setting.Language).HasColumnName("language");
        builder.Entity<Setting>().Property(setting => setting.DarkMode).HasColumnName("darkMode");
        builder.Entity<Setting>().Property(setting => setting.NotificationEnabled).HasColumnName("notificationEnabled");
        builder.Entity<Setting>().Property(setting => setting.CreatedAt).HasColumnName("createdAt");
        builder.Entity<Setting>().Property(setting => setting.UpdatedAt).HasColumnName("updatedAt");
    }
    
}
