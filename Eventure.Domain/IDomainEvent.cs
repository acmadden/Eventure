using System;
using MediatR;

namespace Eventure.Domain
{
    public interface IDomainEvent : INotification
    {
        DateTime CreatedAt { get; set; }
    }
}