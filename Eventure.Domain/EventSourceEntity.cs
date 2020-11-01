using System.Collections.Generic;

namespace Eventure.Domain
{
    public abstract class EventSourceEntity
    {
        private readonly List<IDomainEvent> _changes = new List<IDomainEvent>();
        public int Version { get; internal set; }
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _changes.AsReadOnly();

        protected EventSourceEntity() { }

        public void Initialize(IEnumerable<IDomainEvent> changes)
        {
            if (changes == null)
            {
                return;
            }
            foreach (var change in changes)
            {
                Mutate(change);
                Version++;
            }
        }

        protected void AddDomainEvent(IDomainEvent @event) => _changes.Add(@event);

        protected void RemoveDomainEvent(IDomainEvent @event) => _changes.Remove(@event);

        protected void ClearDomainEvents() => _changes.Clear();

        protected void Apply(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
            {
                Apply(@event);
            }
        }

        public void Apply(IDomainEvent @event)
        {
            Mutate(@event);
            AddDomainEvent(@event);
        }

        private void Mutate(IDomainEvent @event) => ((dynamic)this).On((dynamic)@event);
    }
}