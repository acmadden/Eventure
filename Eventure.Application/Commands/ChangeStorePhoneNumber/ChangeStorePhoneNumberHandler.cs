using System.Threading;
using System.Threading.Tasks;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using MediatR;

namespace Eventure.Application.Commands
{
    public class ChangeStorePhoneNumberHandler : IRequestHandler<ChangeStorePhoneNumberCommand>
    {
        private readonly IEventStoreRepository<Store> _repository;

        public ChangeStorePhoneNumberHandler(IEventStoreRepository<Store> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(ChangeStorePhoneNumberCommand command, CancellationToken cancellationToken)
        {
            var store = await _repository.FetchAsync(command.AggregateId);
            store.ChangePhoneNumber(command.PhoneNumber);
            await _repository.SaveAsync(store);
            return Unit.Value;
        }
    }
}