using System.Threading.Tasks;
using Eventure.Domain;

namespace Eventure.Application.Repositories
{
    public interface IRepository<TAggregate, TId> where TAggregate : IAggregateRoot<TId>
    {
        Task SaveAsync(TAggregate aggregate);

        Task<TAggregate> FetchAsync(TId aggregateId);
    }
}