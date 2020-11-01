using System;
using System.Linq;
using System.Threading.Tasks;
using Eventure.Application.Repositories;
using Eventure.Domain;
using Eventure.Infrastructure.EventStore.Dao;
using MediatR;
using MongoDB.Driver;

namespace Eventure.Infrastructure.EventStore.Repositories
{
    public class EventStoreRepository<TAggregate> : IEventStoreRepository<TAggregate> where TAggregate : EventSourceEntity, IAggregateRoot, new()
    {
        protected readonly Context _context;
        private readonly IMediator _mediator;

        public EventStoreRepository(Context context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<TAggregate> FetchAsync(Guid aggregateId)
        {
            var events = await _context.Events.Find<EventDao>(x => x.AggregateId == aggregateId).ToListAsync();
            var domainEvents = events.Select(e => e.Data);
            var aggregate = new TAggregate();
            aggregate.Initialize(domainEvents);
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
                await _mediator.Publish(domainEvent);
            }
        }
    }
}