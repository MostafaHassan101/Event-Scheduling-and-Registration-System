using EventSystem.Domain.ValueObjects.Base;

namespace EventSystem.Domain.ValueObjects
{
    public class EventDate : ValueObject
    {
        public DateTime Value { get; private set; }

        private EventDate() { }

        public EventDate(DateTime value)
        {
            if (value < DateTime.UtcNow.Date)
                throw new ArgumentException("Event date cannot be in the past", nameof(value));

            Value = value.Date;
        }

        protected override IEnumerable<object> GetObjectValues()
        {
            yield return Value;
        }
    }
}