using System.Threading.Tasks;
using Eventure.Domain;

namespace Eventure.Application.Repositories
{
    public interface IWriteProjectionRepository<TEvent> where TEvent : IDomainEvent
    {
        Task WriteAsync(TEvent @event);
    }
}