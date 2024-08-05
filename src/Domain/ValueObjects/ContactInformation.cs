using EventSystem.Domain.ValueObjects.Base;
using System.Text.RegularExpressions;

namespace EventSystem.Domain.ValueObjects
{
    public class ContactInformation : ValueObject
    {
        public string Value { get; private set; }

        private ContactInformation() { }

        public ContactInformation(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Phone number cannot be empty", nameof(value));

            if (!IsValidPhoneNumber(value))
                throw new ArgumentException("Invalid phone number format", nameof(value));

            Value = value;
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return Regex.IsMatch(phoneNumber, @"^\+[0-9]\d{11}$|^[0-9]\d{10}$");
        }

        protected override IEnumerable<object> GetObjectValues()
        {
            yield return Value;
        }
    }
}