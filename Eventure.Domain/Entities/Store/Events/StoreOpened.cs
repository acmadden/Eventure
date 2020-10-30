using System;

namespace Eventure.Domain.Entities
{
    public class StoreOpened : IDomainEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public StoreOpened(Guid id, string name)
        {
            Id = id;
            Name = name;
            CreatedAt = DateTime.UtcNow;
        }
    }
}