using System.Threading;
using System.Threading.Tasks;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using MediatR;

namespace Eventure.Application.Commands
{
    public class InstallNodeHandler : IRequestHandler<InstallNodeCommand>
    {
        private readonly IEventStoreRepository<Node> _nodeRepository;
        private readonly IEventStoreRepository<Store> _storeRepository;

        public InstallNodeHandler(IEventStoreRepository<Node> nodeRepository, IEventStoreRepository<Store> storeRepository)
        {
            _nodeRepository = nodeRepository;
            _storeRepository = storeRepository;
        }

        public async Task<Unit> Handle(InstallNodeCommand command, CancellationToken cancellationToken)
        {
            // This logic should be wrapped in a transaction
            var node = Node.InstallNode(command.StoreId, command.Name, command.Number, command.SystemType);
            var store = await _storeRepository.FetchAsync(command.StoreId);
            store.InstallNode(node.Id);
            await _storeRepository.SaveAsync(store);
            await _nodeRepository.SaveAsync(node);
            return Unit.Value;
        }
    }
}