using System;

namespace Eventure.Domain.Entities
{
    public class StoreClosedEvent : IDomainEvent
    {
        public Guid AggregateId { get; set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; set; }

        public StoreClosedEvent(Guid storeId)
        {
            AggregateId = storeId;
            IsActive = false;
            CreatedAt = DateTime.UtcNow;
        }
    }
}