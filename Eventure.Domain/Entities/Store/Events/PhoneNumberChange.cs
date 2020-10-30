using System;

namespace Eventure.Domain.Entities
{
    public class PhoneNumberChange : IDomainEvent
    {
        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; }

        public PhoneNumberChange(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            CreatedAt = DateTime.UtcNow;
        }
    }
}