using System;

namespace Eventure.Domain.Entities
{
    public class StoreOpenedEvent : IDomainEvent
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Location Location { get; set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; set; }

        public StoreOpenedEvent(string name, string phoneNumber, Location location)
        {
            Id = Guid.NewGuid();
            Name = name;
            PhoneNumber = phoneNumber;
            Location = location;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }
    }
}