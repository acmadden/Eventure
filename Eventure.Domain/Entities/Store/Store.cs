using System;
using System.Collections.Generic;
using Eventure.Domain.ValueObjects;

namespace Eventure.Domain.Entities
{
    public class Store : EventSourceEntity, IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Location Location { get; set; }
        public List<Guid> Nodes { get; set; } = new List<Guid>();
        public bool IsActive { get; set; }

        public static Store OpenStore(string name, string phoneNumber, Location location)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Store name was null or empty.");
            }

            if (String.IsNullOrEmpty(phoneNumber))
            {
                throw new ArgumentException("Phone number was null or empty.");
            }

            var store = new Store();
            store.Apply(new StoreOpenedEvent(name, phoneNumber, location));
            return store;
        }

        public void ChangePhoneNumber(string phoneNumber)
        {
            if (PhoneNumber == phoneNumber)
            {
                throw new ArgumentException("Phone number was unchanged.");
            }

            Apply(new StorePhoneNumberChangedEvent(phoneNumber) { AggregateId = Id });
        }

        public void InstallNode(Guid nodeId)
        {
            if (Nodes.Contains(nodeId))
            {
                throw new ArgumentException("Node already installed at store.");
            }

            Apply(new StoreInstalledNodeEvent(nodeId));
        }

        public void CloseStore()
        {
            if (!IsActive)
            {
                throw new ArgumentException("Store is already closed.");
            }
            Apply(new StoreClosedEvent(Id));
        }

        public void On(Store snapshot)
        {
            Id = snapshot.Id;
            Version = snapshot.Version;
            Name = snapshot.Name;
            PhoneNumber = snapshot.PhoneNumber;
            Location = snapshot.Location;
            IsActive = snapshot.IsActive;
        }

        public void On(StoreOpenedEvent @event)
        {
            Id = @event.AggregateId;
            Name = @event.Name;
            PhoneNumber = @event.PhoneNumber;
            Location = @event.Location;
            IsActive = @event.IsActive;
        }

        public void On(StorePhoneNumberChangedEvent @event)
        {
            PhoneNumber = @event.PhoneNumber;
        }

        public void On(StoreInstalledNodeEvent @event)
        {
            Nodes.Add(@event.NodeId);
        }

        public void On(StoreClosedEvent @event)
        {
            IsActive = @event.IsActive;
        }
    }
}