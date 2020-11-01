using System;
using System.Threading;
using System.Threading.Tasks;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using MediatR;

namespace Eventure.Application.Commands
{
    public class PhoneNumberChangeHandler : IRequestHandler<PhoneNumberChangeCommand>
    {
        private readonly IEventStoreRepository<Store, Guid> _repository;

        public PhoneNumberChangeHandler(IEventStoreRepository<Store, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(PhoneNumberChangeCommand request, CancellationToken cancellationToken)
        {
            var store = await _repository.FetchAsync(request.Id);
            store.PhoneNumberChange(request.PhoneNumber);
            await _repository.SaveAsync(store);
            return Unit.Value;
        }
    }
}