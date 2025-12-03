using com.split.backend.IAM.Domain.Model.Aggregates;
using com.split.backend.IAM.Domain.Model.ValueObjects;
using com.split.backend.IAM.Domain.Repositories;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Configuration;
using com.split.backend.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace com.split.backend.IAM.Infrastructure.Persistence.EFC.Repositories;

public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> FindByEmailAsync(string email)
    {
        var normalizedEmail = EmailAddress.Normalize(email);
        return await Context.Set<User>()
            .Include(u => u.Email)
            .Include(u => u.PersonName)
            .Where(user => user.Email.Address == normalizedEmail)
            .FirstOrDefaultAsync();
    }

    public bool ExistsByEmail(string email)
    {
        var normalizedEmail = EmailAddress.Normalize(email);
        return Context.Set<User>().Any(user => user.Email.Address.Equals(normalizedEmail));
    }

    public async Task<User?> FindByHouseHoldIdAsync(string houseHoldId)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(user => user.HouseholdId.Equals(houseHoldId));

    }

    public new async Task<IEnumerable<User>> ListAsync()
    {
        return await Context.Set<User>()
            .Include(u => u.Email)
            .Include(u => u.PersonName)
            .ToListAsync();
    }

    public new async Task<User?> FindByIdAsync(int id)
    {
        return await Context.Set<User>()
            .Include(u => u.Email)
            .Include(u => u.PersonName)
            .FirstOrDefaultAsync(u => u.Id.Equals(id));
    }
}