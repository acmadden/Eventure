using System;
using System.Collections.Generic;

namespace Eventure.Domain.Entities
{
    public class Store : EventSourceEntity, IAggregateRoot<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Location Location { get; set; }

        public Store() { }

        public Store(IEnumerable<IDomainEvent> events) : base(events) { }

        public static Store OpenStore(string name)
        {
            var store = new Store();
            store.Apply(new StoreOpened(Guid.NewGuid(), name));
            return store;
        }

        public void On(StoreOpened @event)
        {
            Id = @event.Id;
            Name = @event.Name;
        }
    }
}