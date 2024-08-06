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
        public override async Task<Event> GetByIdAsync(int id)
        {
            return await _dbContext.Events.Include(e => e.Participants).FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task AddBulk(IEnumerable<Event> events)
        {
            await _dbContext.AddRangeAsync(events);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Event>> GetAllEventsByUserIdAsync(int userId)
        {
            var result = await _dbContext.Events
                .Include(e => e.Participants)
                .Where(e => e.Participants.Any(u => u.Id == userId))
                .ToListAsync();
            return result;
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
