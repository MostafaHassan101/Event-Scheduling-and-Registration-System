using EventSystem.Domain.Entities.Base;

namespace EventSystem.Domain.Entities
{
    public class User : AggregateRoot
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public ContactInformation ContactInformation { get; private set; }

        private readonly List<Event> _events = new List<Event>();
        public IReadOnlyCollection<Event> Events => _events.AsReadOnly();

        private User() { }

        public User(string name, string email, ContactInformation phoneNumber)
        {
            Name = name;
            Email = CheckEmailValidation(email);
            ContactInformation = phoneNumber;
        }

        public void AddEvent(Event @event)
        {
            if (!_events.Contains(@event))
            {
                _events.Add(@event);
                @event.AddParticipant(this);
            }
        }

        public void RemoveEvent(Event @event)
        {
            if (_events.Remove(@event))
            {
                @event.RemoveParticipant(this);
            }
        }

        public void Update(string name, string email, ContactInformation phoneNumber)
        {
            Name = name;
            Email = CheckEmailValidation(email);
            ContactInformation = phoneNumber;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }
        public void UpdateEmail(string email)
        {
            Email = CheckEmailValidation(email);
        }
        private string CheckEmailValidation(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty", nameof(email));

            if (!IsValidEmail(email))
                throw new ArgumentException("Invalid email format", nameof(email));
            return email;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email); // or may validate by using Regex
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public void UpdatePhoneNumber(ContactInformation phoneNumber)
        {
            ContactInformation = phoneNumber;
        }
    }
}