using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using EventSystem.Domain.ValueObjects;
using EventSystem.Infrastructure.Persistence;
using EventSystem.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace EventSystem.Infrastructure.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _dbContext.DomainUsers
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetUsersByEventIdAsync(int eventId)
        {
            return await _dbContext.DomainUsers
                .Where(u => u.Events.Any(e => e.Id == eventId))
                .ToListAsync();
        }
    }

}
