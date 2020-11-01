using System.Threading;
using System.Threading.Tasks;
using Eventure.Domain.Entities;
using MediatR;

namespace Eventure.Application.Projections
{
    public class StoreOpenedEventHandler : INotificationHandler<StoreOpened>
    {
        public Task Handle(StoreOpened notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}