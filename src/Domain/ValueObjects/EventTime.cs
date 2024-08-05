using EventSystem.Domain.ValueObjects.Base;

namespace EventSystem.Domain.ValueObjects
{
    public class EventTime : ValueObject
    {
        public TimeSpan Value { get; private set; }

        private EventTime() { }

        public EventTime(TimeSpan value)
        {
            if (value.TotalSeconds < 0 || value.TotalHours >= 24)
                throw new ArgumentException("Invalid time", nameof(value));

            Value = value;
        }

        public EventTime(int hour, int minute)
        {
            if (hour < 0 || hour > 23 || minute < 0 || minute > 59)
                throw new ArgumentException("Invalid time");

            Value = new TimeSpan(hour, minute, 0);
        }

        protected override IEnumerable<object> GetObjectValues()
        {
            yield return Value;
        }
    }
}