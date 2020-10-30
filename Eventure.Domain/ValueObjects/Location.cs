using System.Collections.Generic;

namespace Eventure.Domain
{
    public class Location : ValueObject
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return PostalCode;
        }
    }
}