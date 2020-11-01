using System.Threading;
using System.Threading.Tasks;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using MediatR;

namespace Eventure.Application.Projections
{
    public class NodeInstalledProjectionHandler : INotificationHandler<NodeInstalledEvent>
    {
        private readonly IProjectionRepository<NodeInstalledEvent> _repository;

        public NodeInstalledProjectionHandler(IProjectionRepository<NodeInstalledEvent> repository)
        {
            _repository = repository;
        }

        public async Task Handle(NodeInstalledEvent @event, CancellationToken cancellationToken)
        {
            await _repository.ProjectAsync(@event);
        }
    }
}