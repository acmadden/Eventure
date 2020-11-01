using System;
using System.Threading.Tasks;
using Eventure.Domain;

namespace Eventure.Application.Repositories
{
    public interface IEventStoreRepository<TAggregate> where TAggregate : IAggregateRoot
    {
        Task SaveAsync(TAggregate aggregate);

        Task<TAggregate> FetchAsync(Guid aggregateId);
    }
}