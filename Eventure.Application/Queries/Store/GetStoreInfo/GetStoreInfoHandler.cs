using System.Threading;
using System.Threading.Tasks;
using Eventure.Application.Projection.ReadModels;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using MediatR;

namespace Eventure.Application.Queries
{
    public class GetStoreInfoHandler : IRequestHandler<GetStoreInfoQuery, StoreReadModel>
    {
        private readonly IReadProjectionRepository<StoreReadModel> _repository;

        public GetStoreInfoHandler(IReadProjectionRepository<StoreReadModel> repository)
        {
            _repository = repository;
        }

        public async Task<StoreReadModel> Handle(GetStoreInfoQuery request, CancellationToken cancellationToken)
        {
            return await _repository.FetchByIdAsync(request.Id);
        }
    }
}