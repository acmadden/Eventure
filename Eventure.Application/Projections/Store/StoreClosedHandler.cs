using System.Threading;
using System.Threading.Tasks;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using MediatR;

namespace Eventure.Application.Projections
{
    public class StoreClosedHandler : INotificationHandler<StoreClosedEvent>
    {
        private readonly IWriteProjectionRepository<StoreClosedEvent> _repository;

        public StoreClosedHandler(IWriteProjectionRepository<StoreClosedEvent> repository)
        {
            _repository = repository;
        }

        public async Task Handle(StoreClosedEvent @event, CancellationToken cancellationToken)
        {
            await _repository.WriteAsync(@event);
        }
    }
}