using System;

namespace Eventure.Domain.Entities
{
    public class Node : EventSourceEntity, IAggregateRoot
    {
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string SystemType { get; set; }
        public NodeStatus Status { get; set; }

        public static Node InstallNode(Guid storeId, string name, string number, string systemType)
        {
            var node = new Node();
            node.Apply(new NodeInstalledEvent(storeId, name, number, systemType));
            return node;
        }

        public void On(Node snapshot)
        {
            Id = snapshot.Id;
            Version = snapshot.Version;
            StoreId = snapshot.StoreId;
            Name = snapshot.Name;
            Number = snapshot.Number;
            SystemType = snapshot.SystemType;
            Status = snapshot.Status;
        }

        public void On(NodeInstalledEvent @event)
        {
            Id = @event.AggregateId;
            StoreId = @event.StoreId;
            Name = @event.Name;
            Number = @event.Number;
            SystemType = @event.SystemType;
            Status = @event.Status;
        }

    }
}