using System;
using System.Threading;
using System.Threading.Tasks;
using Eventure.Application.Repositories;
using Eventure.Domain;
using Eventure.Domain.Entities;
using MediatR;

namespace Eventure.Application.Commands
{
    public class OpenStoreHandler : IRequestHandler<OpenStoreCommand, Store>
    {
        private readonly IEventStoreRepository<Store, Guid> _repository;

        public OpenStoreHandler(IEventStoreRepository<Store, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<Store> Handle(OpenStoreCommand request, CancellationToken cancellationToken)
        {
            var store = Store.OpenStore(request.Name, request.PhoneNumber, new Location()
            {
                Street = request.Street,
                City = request.City,
                State = request.State,
                PostalCode = request.PostalCode,
                Country = request.Country
            });
            await _repository.SaveAsync(store);
            return store;
        }
    }
}