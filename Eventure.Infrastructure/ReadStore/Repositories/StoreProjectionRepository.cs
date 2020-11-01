using System.Threading.Tasks;
using Dapper;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using Eventure.Infrastructure.ReadStore.Settings;
using Microsoft.Extensions.Options;

namespace Eventure.Infrastructure.ReadStore.Repositories
{
    public class StoreProjectionRepository : ProjectionRepositoryBase, IProjectionRepository<StoreOpenedEvent>
    {
        public StoreProjectionRepository(IOptions<SqlServerSettings> options) : base(options) { }

        public async Task ProjectAsync(StoreOpenedEvent @event)
        {
            string sql = @"INSERT INTO dbo.Stores(Id, Name, PhoneNumber, Street, City, State, PostalCode, Country, IsActive) 
                VALUES(@Id, @Name, @PhoneNumber, @Street, @City, @State, @PostalCode, @Country, @IsActive)";
            var parameters = new
            {
                Id = @event.Id,
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
    }
}