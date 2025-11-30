using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
        
        builder.Entity<EmailAddress>().HasKey(e => e.Id);
        builder.Entity<EmailAddress>().Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Entity<EmailAddress>().Property(e => e.Address).HasMaxLength(100).IsRequired();
        
        builder.Entity<User>().Property(u => u.Password).IsRequired();
        
        builder.Entity<PersonName>().HasKey(p => p.Id);
        builder.Entity<PersonName>().Property(p => p.Id).ValueGeneratedOnAdd();
        builder.Entity<PersonName>().Property(p => p.FirstName);
        builder.Entity<PersonName>().Property(p => p.LastName);
        
        builder.Entity<User>().Property(u => u.HouseholdId).IsRequired();

        builder.Entity<User>().Property(u => u.CreatedDate);
        builder.Entity<User>().Property(u => u.UpdatedDate);
    }
}