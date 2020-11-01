using Eventure.Domain;

namespace Eventure.Application.Repositories
{
    public interface IProjectionRepository<TEvent> where TEvent : IDomainEvent { }
}