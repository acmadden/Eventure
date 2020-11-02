using System.Threading.Tasks;
using Dapper;
using Eventure.Application.Projection.ReadModels;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using Eventure.Infrastructure.Projection.Settings;
using Microsoft.Extensions.Options;

namespace Eventure.Infrastructure.Projection.Repositories
{
    public class StoreWriteProjectionRepository : ProjectionRepositoryBase,
        IWriteProjectionRepository<StoreOpenedEvent>,
        IWriteProjectionRepository<StorePhoneNumberChangedEvent>,
        IWriteProjectionRepository<StoreClosedEvent>
    {
        public StoreWriteProjectionRepository(IOptions<SqlServerSettings> options) : base(options) { }

        public async Task WriteAsync(StoreOpenedEvent @event)
        {
            string sql = @"INSERT INTO dbo.Stores(Id, Name, PhoneNumber, Street, City, State, PostalCode, Country, IsActive) 
                VALUES(@Id, @Name, @PhoneNumber, @Street, @City, @State, @PostalCode, @Country, @IsActive)";
            var parameters = new StoreReadModel
            {
                Id = @event.AggregateId,
                Name = @event.Name,
                PhoneNumber = @event.PhoneNumber,
                Street = @event.Location.Street,
                City = @event.Location.City,
                State = @event.Location.State,
                PostalCode = @event.Location.PostalCode,
                Country = @event.Location.Country,
                IsActive = @event.IsActive
            };
            await _connection.ExecuteAsync(sql, parameters);
        }

        public async Task WriteAsync(StorePhoneNumberChangedEvent @event)
        {
            var sql = @"UPDATE dbo.Stores SET PhoneNumber = @PhoneNumber WHERE Id = @AggregateId";
            await _connection.ExecuteAsync(sql, @event);
        }

        public async Task WriteAsync(StoreClosedEvent @event)
        {
            var sql = @"UPDATE dbo.Stores SET IsActive = @IsActive WHERE Id = @AggregateId";
            await _connection.ExecuteAsync(sql, @event);
        }
    }
}