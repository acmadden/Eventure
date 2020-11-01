using System.Threading;
using System.Threading.Tasks;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using MediatR;

namespace Eventure.Application.Projections
{
    public class ChangeStorePhoneNumberProjectionHandler : INotificationHandler<StorePhoneNumberChangeEvent>
    {
        private readonly IProjectionRepository<StorePhoneNumberChangeEvent> _repository;

        public ChangeStorePhoneNumberProjectionHandler(IProjectionRepository<StorePhoneNumberChangeEvent> repository)
        {
            _repository = repository;
        }

        public async Task Handle(StorePhoneNumberChangeEvent @event, CancellationToken cancellationToken)
        {
            await _repository.ProjectAsync(@event);
        }
    }
}
