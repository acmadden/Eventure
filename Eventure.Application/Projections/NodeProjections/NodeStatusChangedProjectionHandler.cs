using System.Threading;
using System.Threading.Tasks;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using MediatR;

namespace Eventure.Application.Projections
{
    public class NodeStatusChangedProjectionHandler : INotificationHandler<NodeStatusChangedEvent>
    {
        private readonly IWriteProjectionRepository<NodeStatusChangedEvent> _repository;

        public NodeStatusChangedProjectionHandler(IWriteProjectionRepository<NodeStatusChangedEvent> repository)
        {
            _repository = repository;
        }

        public async Task Handle(NodeStatusChangedEvent @event, CancellationToken cancellationToken)
        {
            await _repository.WriteAsync(@event);
        }
    }
}