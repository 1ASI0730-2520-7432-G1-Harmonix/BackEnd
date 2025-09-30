using Mysqlx.Crud;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace com.split.backend.Households.Domain.Models.Commands;

public record CreateHouseholdCommand(
    string Name,
    string Description,
    string Currency,
    long RepresentativeId
)
{
    private static readonly ISet<String>? SUPPORTED_CURRENCIES = new HashSet<String>
    {
        "USD",
        "PEN",
        "EUR",
        "GBP",
    };

    public CreateHouseholdCommand
    {
        if (String.IsNullOrEmpty(Name))
        {
            throw new ArgumentNullException("Name cannot be null.");
        }

        if (String.IsNullOrEmpty(Description))
        {
            throw new ArgumentException("Description cannot be null.");
        }

        if (String.IsNullOrEmpty(Currency) || SUPPORTED_CURRENCIES.Contains(Currency))
        {
            throw new ArgumentException("Currency cannot be null.");
        }

        if (RepresentativeId <= 0 || RepresentativeId > Int32.MaxValue || RepresentativeId == null)
        {
            throw new ArgumentException("RepresentativeId cannot be null or greater than Int32.MaxValue.");
        }
    }
    
};