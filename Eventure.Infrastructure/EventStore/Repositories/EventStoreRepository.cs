using System;
using System.Linq;
using System.Threading.Tasks;
using Eventure.Application.Repositories;
using Eventure.Domain;
using Eventure.Infrastructure.EventStore.Daos;
using Eventure.Infrastructure.EventStore.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Eventure.Infrastructure.EventStore.Repositories
{
    public class EventStoreRepository<TAggregate> : IEventStoreRepository<TAggregate> where TAggregate : EventSourceEntity, IAggregateRoot, new()
    {
        protected readonly Context _context;
        private readonly IMediator _mediator;
        private readonly EventStoreSettings _settings;

        public EventStoreRepository(Context context, IMediator mediator, IOptions<EventStoreSettings> options)
        {
            _context = context;
            _mediator = mediator;
            _settings = options.Value;
        }

        public async Task<TAggregate> FetchAsync(Guid aggregateId)
        {
            var snapshot = await _context.Snapshots
                .Find<SnapshotDao>(x => x.AggregateId == aggregateId)
                .SortByDescending(x => x.Version)
                .FirstOrDefaultAsync();

            var events = await _context.Events
                .Find<EventDao>(x => x.AggregateId == aggregateId && x.Version > snapshot.Version)
                .ToListAsync();

            var domainEvents = events.Select(e => e.Data);
            var aggregate = new TAggregate();
            aggregate.Initialize(snapshot.Data, domainEvents);
            return aggregate;
        }

        public async Task SaveAsync(TAggregate aggregate)
        {

            foreach (var domainEvent in aggregate.DomainEvents)
            {
                var @event = new EventDao()
                {
                    AggregateId = aggregate.Id,
                    Aggregate = typeof(TAggregate).FullName,
                    Version = aggregate.Version,
                    Data = domainEvent,
                    CreatedAt = domainEvent.CreatedAt
                };
                await _context.Events.InsertOneAsync(@event);

                if (aggregate.Version > 0 && aggregate.Version % _settings.SnapshotInterval == 0)
                {
                    var snapshot = new SnapshotDao()
                    {
                        AggregateId = aggregate.Id,
                        Version = aggregate.Version,
                        Data = aggregate,
                        CreatedAt = DateTime.UtcNow
                    };
                    await _context.Snapshots.InsertOneAsync(snapshot);
                }

                await _mediator.Publish(domainEvent);
            }
        }
    }
}