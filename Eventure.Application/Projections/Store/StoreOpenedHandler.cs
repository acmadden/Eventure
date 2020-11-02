using System.Threading;
using System.Threading.Tasks;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using MediatR;

namespace Eventure.Application.Projections
{
    public class StoreOpenedHandler : INotificationHandler<StoreOpenedEvent>
    {
        private readonly IWriteProjectionRepository<StoreOpenedEvent> _repository;

        public StoreOpenedHandler(IWriteProjectionRepository<StoreOpenedEvent> repository)
        {
            _repository = repository;
        }
        public async Task Handle(StoreOpenedEvent @event, CancellationToken cancellationToken)
        {
            await _repository.WriteAsync(@event);
        }
    }
}