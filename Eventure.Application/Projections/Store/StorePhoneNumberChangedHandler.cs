using System.Threading;
using System.Threading.Tasks;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using MediatR;

namespace Eventure.Application.Projections
{
    public class StorePhoneNumberChangedHandler : INotificationHandler<StorePhoneNumberChangedEvent>
    {
        private readonly IWriteProjectionRepository<StorePhoneNumberChangedEvent> _repository;

        public StorePhoneNumberChangedHandler(IWriteProjectionRepository<StorePhoneNumberChangedEvent> repository)
        {
            _repository = repository;
        }

        public async Task Handle(StorePhoneNumberChangedEvent @event, CancellationToken cancellationToken)
        {
            await _repository.WriteAsync(@event);
        }
    }
}
