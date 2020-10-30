using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using Eventure.Infrastructure.Dao;
using MongoDB.Driver;

namespace Eventure.Infrastructure.Mongo.Repositories
{
    public class StoreRepository : IRepository<Store, Guid>
    {
        private readonly Context _context;

        public StoreRepository(Context context)
        {
            _context = context;
        }

        public async Task SaveAsync(Store aggregate)
        {
            var events = new List<EventDao>();
            foreach (var domainEvent in aggregate.DomainEvents)
            {
                events.Add(new EventDao()
                {
                    AggregateId = aggregate.Id,
                    Aggregate = typeof(Store).FullName,
                    Version = aggregate.Version,
                    Data = domainEvent,
                    CreatedAt = domainEvent.CreatedAt
                });
            }
            await _context.Events.InsertManyAsync(events);
        }

        public async Task<Store> FetchAsync(Guid aggregateId)
        {
            var events = await _context.Events.Find<EventDao>(x => x.AggregateId == aggregateId).ToListAsync();
            var domainEvents = events.Select(e => e.Data);
            return new Store(domainEvents);
        }
    }
}