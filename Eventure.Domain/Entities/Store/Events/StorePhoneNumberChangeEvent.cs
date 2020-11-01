using System;

namespace Eventure.Domain.Entities
{
    public class StorePhoneNumberChangeEvent : IDomainEvent
    {
        public Guid AggregateId { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }

        public StorePhoneNumberChangeEvent(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            CreatedAt = DateTime.UtcNow;
        }
    }
}