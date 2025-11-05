using com.split.backend.Households.Domain.Models.Aggregates;
using com.split.backend.IAM.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.Households.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyHouseHoldConfiguration(this ModelBuilder builder)
    {
        builder.Entity<HouseHold>().HasKey(p => p.Id);
        builder.Entity<HouseHold>().Property(p => p.Id).IsRequired();
        builder.Entity<HouseHold>().Property(p => p.Name);
        builder.Entity<HouseHold>().Property(p => p.Currency).IsRequired();
        builder.Entity<HouseHold>().Property(p => p.RepresentativeId).IsRequired();
    }
    
}