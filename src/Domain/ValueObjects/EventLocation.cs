
using EventSystem.Domain.ValueObjects.Base;
using System.Xml.Linq;

namespace EventSystem.Domain.ValueObjects
{
    public class EventLocation : ValueObject
    {
        public string Address { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public string PostalCode { get; private set; }

        public EventLocation(string address, string city, string country, string postalCode)
        {
            Address = CheckAddress(address);
            City = city;
            Country = country;
            PostalCode = postalCode;
        }
        private EventLocation() { }

        string CheckAddress(string address)
        {
            if(string.IsNullOrEmpty(address))
                throw new ArgumentNullException("address");
            return address;
        }
        protected override IEnumerable<object> GetObjectValues()
        {
            yield return Address;
            yield return City;
            yield return Country;
            yield return PostalCode;
        }
    }
}