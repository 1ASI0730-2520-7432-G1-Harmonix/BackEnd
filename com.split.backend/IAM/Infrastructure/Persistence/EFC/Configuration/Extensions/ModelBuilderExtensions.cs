using com.split.backend.IAM.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.IAM.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).ValueGeneratedOnAdd();
        builder.Entity<User>().OwnsOne(p => p.Email,
            n =>
            {
                n.WithOwner().HasForeignKey("Id");
                n.Property(p => p.Address).HasColumnName("EmailAddress");
            });
        builder.Entity<User>().Property(u => u.Password).IsRequired();
        builder.Entity<User>().OwnsOne(p => p.PersonName,
            n =>
            {
                n.WithOwner().HasForeignKey("Id");
                n.Property(p => p.FirstName).HasColumnName("FirstName");
                n.Property(p => p.LastName).HasColumnName("LastName");
            });
        builder.Entity<User>().Property(u => u.HouseholdId).IsRequired();
    }
}