
using EventSystem.Domain.Entities.Base;

namespace EventSystem.Domain.Entities
{
    public class EventRegistration : AggregateRoot
    {
        public int UserId { get; private set; }
        public int EventId { get; private set; }
        public DateTime RegistrationDate { get; private set; }

        private EventRegistration() { }

        public EventRegistration(int userId, int eventId)
        {
            UserId = userId;
            EventId = eventId;
            RegistrationDate = DateTime.UtcNow;
        }
    }
}