using System.Threading;
using System.Threading.Tasks;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using MediatR;

namespace Eventure.Application.Commands
{
    public class InstallNodeHandler : IRequestHandler<InstallNodeCommand>
    {
        private readonly IEventStoreRepository<Node> _repository;

        public InstallNodeHandler(IEventStoreRepository<Node> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(InstallNodeCommand command, CancellationToken cancellationToken)
        {
            var node = Node.InstallNode(command.StoreId, command.Name, command.Number, command.SystemType);
            await _repository.SaveAsync(node);
            return Unit.Value;
        }
    }
}