using com.split.backend.HouseholdMembers.Domain.Models.Aggregates;
using com.split.backend.HouseholdMembers.Domain.Repositories;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.HouseholdMembers.Infrastructure.Persistence.EFC.Repositories;

public class HouseholdMemberRepository(AppDbContext context) 
    : BaseRepository<HouseholdMember>(context), IHouseholdMemberRepository
{
    public async Task<HouseholdMember?> FindByIdAsync(int id)
    {
        return await Context.Set<HouseholdMember>().FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<HouseholdMember?> FindByHouseholdIdAndUserIdAsync(string householdId, int userId)
    {
        return await Context.Set<HouseholdMember>()
            .FirstOrDefaultAsync(m => m.HouseholdId == householdId && m.UserId == userId);
    }

    public async Task<IEnumerable<HouseholdMember>> FindByHouseholdIdAsync(string householdId)
    {
        return await Context.Set<HouseholdMember>()
            .Where(m => m.HouseholdId == householdId)
            .ToListAsync();
    }

    public async Task<IEnumerable<HouseholdMember>> FindByUserIdAsync(int userId)
    {
        return await Context.Set<HouseholdMember>()
            .Where(m => m.UserId == userId)
            .ToListAsync();
    }

    public async Task<HouseholdMember?> FindRepresentativeByHouseholdIdAsync(string householdId)
    {
        return await Context.Set<HouseholdMember>()
            .FirstOrDefaultAsync(m => m.HouseholdId == householdId && m.IsRepresentative);
    }

    public bool ExistsByHouseholdIdAndUserId(string householdId, int userId)
    {
        return Context.Set<HouseholdMember>()
            .Any(m => m.HouseholdId == householdId && m.UserId == userId);
    }
}

