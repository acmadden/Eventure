using System;
using Eventure.Domain.ValueObjects;

namespace Eventure.Domain.Entities
{
    public class NodeInstalledEvent : IDomainEvent
    {
        public Guid AggregateId { get; set; }
        public Guid StoreId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string SystemType { get; set; }
        public NodeStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public NodeInstalledEvent(Guid storeId, string name, string number, string systemType)
        {
            AggregateId = Guid.NewGuid();
            StoreId = storeId;
            Name = name;
            Number = number;
            SystemType = systemType;
            Status = new NodeStatus() { Offline = true, Reason = "Install" };
            CreatedAt = DateTime.UtcNow;
        }
    }
}