using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Eventure.Application.Projection.ReadModels;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using Eventure.Domain.ValueObjects;
using MediatR;

namespace Eventure.Application.Commands
{
    public class CloseStoreHandler : IRequestHandler<CloseStoreCommand>
    {
        private readonly IEventStoreRepository<Store> _storeRepository;

        public CloseStoreHandler(IEventStoreRepository<Store> storeRepository)
        {
            _storeRepository = storeRepository;
        }

        public async Task<Unit> Handle(CloseStoreCommand command, CancellationToken cancellationToken)
        {
            var store = await _storeRepository.FetchAsync(command.Id);
            store.CloseStore();
            await _storeRepository.SaveAsync(store);
            return Unit.Value;
        }
    }
}