using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using EventSystem.Infrastructure.Persistence;
using EventSystem.Infrastructure.Repository.Base;

namespace EventSystem.Infrastructure.Repository
{
    public class EventRegistrationRepository : Repository<EventRegistration>, IEventRegistrationRepository
    {
        public EventRegistrationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }

}
