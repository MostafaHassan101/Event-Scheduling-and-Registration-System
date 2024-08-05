using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using EventSystem.Domain.ValueObjects;
using EventSystem.Infrastructure.Persistence;
using EventSystem.Infrastructure.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace EventSystem.Infrastructure.Repository
{
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Event>> GetAllEventsByUserIdAsync(int userId)
        {
            return await _dbContext.Events
                .Include(e => e.Participants)
                .Where(e => e.Participants.Any(u => u.Id == userId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetEventsByDateAsync(EventDate date)
        {
            return await _dbContext.Events
                .Where(e => e.Date == date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetEventsByLocationAsync(EventLocation location)
        {
            return await _dbContext.Events
                .Where(e => e.Location == location)
                .ToListAsync();
        }

    }
}
