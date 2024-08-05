using EventSystem.Domain.Entities.Base;
using EventSystem.Domain.Interfaces;

namespace EventSystem.Domain.Entities
{
    public class Event : AggregateRoot
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public EventDate Date { get; private set; }
        public EventTime Time { get; private set; }
        public EventLocation Location { get; private set; }

        private readonly List<User> _participants = new List<User>();
        public IReadOnlyCollection<User> Participants => _participants.AsReadOnly();


        private Event() { }

        public Event(string title, string description, EventDate date, EventTime time, EventLocation location)
        {
            Title = title;
            Description = description;
            Date = date;
            Time = time;
            Location = location;
        }

        public void AddParticipant(User user)
        {
            if (!_participants.Contains(user))
            {
                _participants.Add(user);
            }
        }

        public void RemoveParticipant(User user)
        {
            _participants.Remove(user);
        }

        public void Update(string title, string description, EventDate date, EventTime time, EventLocation location)
        {
            Title = title;
            Description = description;
            Date = date;
            Time = time;
            Location = location;
        }

        public void UpdateTitle(string title)
        {
            Title = title;
        }

        public void UpdateDescription(string description)
        {
            Description = description;
        }
        public void UpdateDate(EventDate eventDate)
        {
            Date = eventDate;
        }
        public void UpdateTime(EventTime time)
        {
            Time = time;
        }
        public void UpdateLocation(EventLocation location)
        { 
            Location = location; 
        }

    }
}