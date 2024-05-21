using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.ValueObjects
{
    public class Address //:value object
    {
        public string AddressLine { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        public int CityCode { get; private set; }

        public Address(string addressLine, string city, string country, int cityCode)
        {
            AddressLine = addressLine;
            City = city;
            Country = country;
            CityCode = cityCode;
        }

        public override bool Equals(object obj)
        {
            if (obj is Address other)
            {
                return AddressLine == other.AddressLine &&
                       City == other.City &&
                       Country == other.Country &&
                       CityCode == other.CityCode;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AddressLine, City, Country, CityCode);
        }
    }


}
