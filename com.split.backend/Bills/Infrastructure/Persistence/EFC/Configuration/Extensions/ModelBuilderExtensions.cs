using Microsoft.EntityFrameworkCore;
using com.split.backend.Bills.Domain.Models.Aggregates;

namespace com.split.backend.Bills.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyBillsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Bill>().HasKey(x => x.Id);
        
        builder.Entity<Bill>().Property(x => x.Title)
            .HasMaxLength(160)
            .IsRequired();

        builder.Entity<Bill>().Property(x => x.Category)
            .HasMaxLength(80)
            .HasDefaultValue("General");
        
        builder.Entity<Bill>().Property(x => x.Currency)
            .HasMaxLength(8)
            .HasDefaultValue("PEN");

        builder.Entity<Bill>().Property(x => x.Amount)
            .HasPrecision(18, 2);

        builder.Entity<Bill>().Property(x => x.Status)
            .HasConversion<int>();

        builder.Entity<Bill>().HasIndex(x => new { x.HouseholdId, x.DueDate });

    }
}