using Mysqlx.Crud;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace com.split.backend.Households.Domain.Models.Commands;

public record CreateHouseholdCommand(
    string Name,
    long RepresentativeId,
    string Currency,
    string Description,
    int MemberCount
) ;