
using EventSystem.Domain.ValueObjects.Base;

namespace EventSystem.Domain.ValueObjects
{
    public class EventLocation : ValueObject
    {
        public string Value { get; private set; }

        private EventLocation() { }

        public EventLocation(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Location cannot be empty", nameof(value));

            if (value.Length > 200)
                throw new ArgumentException("Location is too long", nameof(value));

            Value = value;
        }

        protected override IEnumerable<object> GetObjectValues()
        {
            yield return Value;
        }
    }
}