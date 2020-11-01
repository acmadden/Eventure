using System;
using System.Threading;
using System.Threading.Tasks;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using MediatR;

namespace Eventure.Application.Queries
{
    public class GetStoreInfoHandler : IRequestHandler<GetStoreInfoQuery, Store>
    {
        private readonly IEventStoreRepository<Store, Guid> _repository;

        public GetStoreInfoHandler(IEventStoreRepository<Store, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<Store> Handle(GetStoreInfoQuery request, CancellationToken cancellationToken)
        {
            return await _repository.FetchAsync(request.Id);
        }
    }
}