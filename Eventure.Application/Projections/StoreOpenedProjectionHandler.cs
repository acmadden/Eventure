using System.Threading;
using System.Threading.Tasks;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using MediatR;

namespace Eventure.Application.Projections
{
    public class StoreOpenedProjectionHandler : INotificationHandler<StoreOpenedEvent>
    {
        private readonly IProjectionRepository<StoreOpenedEvent> _repository;

        public StoreOpenedProjectionHandler(IProjectionRepository<StoreOpenedEvent> repository)
        {
            _repository = repository;
        }
        public async Task Handle(StoreOpenedEvent @event, CancellationToken cancellationToken)
        {
            await _repository.ProjectAsync(@event);
        }
    }
}