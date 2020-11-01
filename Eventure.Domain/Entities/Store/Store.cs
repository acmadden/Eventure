using System;

namespace Eventure.Domain.Entities
{
    public class Store : EventSourceEntity, IAggregateRoot
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Location Location { get; set; }
        public bool IsActive { get; set; }

        public static Store OpenStore(string name, string phoneNumber, Location location)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new InvalidStoreNameException();
            }

            if (String.IsNullOrEmpty(phoneNumber))
            {
                throw new InvalidPhoneNumberException();
            }

            var store = new Store();
            store.Apply(new StoreOpenedEvent(name, phoneNumber, location));
            return store;
        }

        public void On(StoreOpenedEvent @event)
        {
            Id = @event.Id;
            Name = @event.Name;
            PhoneNumber = @event.PhoneNumber;
            Location = @event.Location;
            IsActive = @event.IsActive;
        }
    }
}