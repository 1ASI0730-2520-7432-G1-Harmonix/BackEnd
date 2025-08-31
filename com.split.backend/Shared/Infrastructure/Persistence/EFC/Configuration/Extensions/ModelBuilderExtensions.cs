using Microsoft.EntityFrameworkCore;

namespace com.split.backend.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

public class ModelBuilderExtensions
{
   public static void UseSnakeCaseNamingConvention(this ModelBuilder builder)
   {
      foreach (var entity in builder.Model.GetEntityTypes())
      {
         var tableName = entity.GetTableName();
         if(!string.IsNullOrEmpty(tableName)) entity.SetTableName(tableName.ToP);
         
      }
   }
}