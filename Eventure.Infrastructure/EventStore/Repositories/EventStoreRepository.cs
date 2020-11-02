using System;
using System.Collections.Generic;
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

            int version = -1;
            ISnapshotData data = null;
            if (snapshot != null)
            {
                version = snapshot.Version;
                data = snapshot.Data;
            }

            var events = await _context.Events
                .Find<EventDao>(x => x.AggregateId == aggregateId && x.Version > version)
                .ToListAsync();

            var domainEvents = events.Select(e => e.Data);
            var aggregate = new TAggregate();
            aggregate.Initialize(data, domainEvents);
            return aggregate;
        }

        public async Task SaveAsync(TAggregate aggregate)
        {
            var events = new List<EventDao>();
            SnapshotDao snapshot = null;
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
                if (aggregate.Version > 0 && aggregate.Version % _settings.SnapshotInterval == 0)
                {
                    snapshot = new SnapshotDao()
                    {
                        AggregateId = aggregate.Id,
                        Version = aggregate.Version,
                        Data = aggregate,
                        CreatedAt = DateTime.UtcNow
                    };
                }
                events.Add(@event);
            }

            await _context.Events.InsertManyAsync(events);

            if (snapshot != null)
            {
                await _context.Snapshots.InsertOneAsync(snapshot);
            }

            foreach (var @event in events)
            {
                await _mediator.Publish(@event.Data);
            }
        }
    }
}