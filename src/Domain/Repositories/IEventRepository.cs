using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories.Base;

namespace EventSystem.Domain.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<IEnumerable<Event>> GetAllEventsByUserIdAsync(int userId);
        Task<IEnumerable<Event>> GetEventsByDateAsync(EventDate date);
        Task<IEnumerable<Event>> GetEventsByLocationAsync(EventLocation location);
    }
}
