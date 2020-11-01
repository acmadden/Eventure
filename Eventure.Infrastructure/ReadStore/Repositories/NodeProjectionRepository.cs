using System.Threading.Tasks;
using Dapper;
using Eventure.Application.Repositories;
using Eventure.Domain.Entities;
using Eventure.Infrastructure.ReadStore.Settings;
using Microsoft.Extensions.Options;

namespace Eventure.Infrastructure.ReadStore.Repositories
{
    public class NodeProjectionRepository : ProjectionRepositoryBase, IProjectionRepository<NodeInstalledEvent>
    {
        public NodeProjectionRepository(IOptions<SqlServerSettings> options) : base(options) { }

        public async Task ProjectAsync(NodeInstalledEvent @event)
        {
            var sql = @"INSERT INTO dbo.Nodes (Id, StoreId, Name, Number, SystemType, Offline, OfflineReason)
                VALUES(@Id, @StoreId, @Name, @Number, @SystemType, @Offline, @OfflineReason);";
            var parameters = new
            {
                Id = @event.AggregateId,
                StoreId = @event.StoreId,
                Name = @event.Name,
                Number = @event.Number,
                SystemType = @event.SystemType,
                @Offline = @event.Status.Offline,
                @OfflineReason = @event.Status.Reason
            };
            await _connection.ExecuteAsync(sql, parameters);
        }
    }
}