using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using Eventure.Infrastructure.EventStore.Dao;
using MediatR;
using MongoDB.Driver;

namespace Eventure.Infrastructure.EventStore.Repositories
{
    public class StoreRepository : IEventStoreRepository<Store, Guid>
    {
        private readonly Context _context;
        private readonly IMediator _mediator;

        public StoreRepository(Context context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task SaveAsync(Store aggregate)
        {

            foreach (var domainEvent in aggregate.DomainEvents)
            {
                var @event = new EventDao()
                {
                    AggregateId = aggregate.Id,
                    Aggregate = typeof(Store).FullName,
                    Version = aggregate.Version,
                    Data = domainEvent,
                    CreatedAt = domainEvent.CreatedAt
                };
                await _context.Events.InsertOneAsync(@event);
                await _mediator.Publish(domainEvent);
            }
        }

        public async Task<Store> FetchAsync(Guid aggregateId)
        {
            var events = await _context.Events.Find<EventDao>(x => x.AggregateId == aggregateId).ToListAsync();
            var domainEvents = events.Select(e => e.Data);
            return new Store(domainEvents);
        }
    }
}