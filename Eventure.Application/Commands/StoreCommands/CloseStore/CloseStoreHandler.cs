using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Eventure.Application.ReadStore.ReadModels;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using Eventure.Domain.ValueObjects;
using MediatR;

namespace Eventure.Application.Commands
{
    public class CloseStoreHandler : IRequestHandler<CloseStoreCommand>
    {
        private readonly IEventStoreRepository<Store> _storeRepository;
        private readonly IEventStoreRepository<Node> _nodeRepository;

        public CloseStoreHandler(IEventStoreRepository<Store> storeRepository, IEventStoreRepository<Node> nodeRepository)
        {
            _storeRepository = storeRepository;
            _nodeRepository = nodeRepository;

        }

        public async Task<Unit> Handle(CloseStoreCommand command, CancellationToken cancellationToken)
        {
            // This logic should be wrapped in a transaction
            var store = await _storeRepository.FetchAsync(command.Id);
            store.CloseStore();
            await _storeRepository.SaveAsync(store);
            var nodes = new List<Node>();
            foreach (var nodeId in store.Nodes)
            {
                var node = await _nodeRepository.FetchAsync(nodeId);
                node.ChangeStatus(new NodeStatus() { Offline = true, Reason = "Store closed." });
                await _nodeRepository.SaveAsync(node);
            }

            return Unit.Value;
        }
    }
}