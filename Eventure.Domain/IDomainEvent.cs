using System;

namespace Eventure.Domain
{
    public interface IDomainEvent
    {
        DateTime CreatedAt { get; set; }
    }
}