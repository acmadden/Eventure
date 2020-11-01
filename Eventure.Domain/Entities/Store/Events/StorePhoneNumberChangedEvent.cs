using System;

namespace Eventure.Domain.Entities
{
    public class StorePhoneNumberChangedEvent : IDomainEvent
    {
        public Guid AggregateId { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }

        public StorePhoneNumberChangedEvent(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            CreatedAt = DateTime.UtcNow;
        }
    }
}