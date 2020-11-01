using System;
using System.Threading.Tasks;
using Eventure.Domain;

namespace Eventure.Application.Repositories
{
    public interface IProjectionRepository<TEvent> where TEvent : IDomainEvent
    {
        Task ProjectAsync(TEvent @event);
    }
}