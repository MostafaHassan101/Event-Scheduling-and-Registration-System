using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories.Base;

namespace EventSystem.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetUsersByEventIdAsync(int eventId);
    }
}
