using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Eventure.Application.ReadStore.ReadModels;
using Eventure.Application.Repositories;
using Eventure.Infrastructure.ReadStore.Settings;
using Microsoft.Extensions.Options;

namespace Eventure.Infrastructure.ReadStore.Repositories
{
    public class StoreReadProjectionRepository : ProjectionRepositoryBase, IReadProjectionRepository<StoreReadModel>
    {
        public StoreReadProjectionRepository(IOptions<SqlServerSettings> options) : base(options) { }

        public async Task<StoreReadModel> FetchByIdAsync(Guid id)
        {
            string query = @"SELECT stores.*, nodes.Id, nodes.Name, nodes.Number, nodes.SystemType, nodes.Offline, nodes.OfflineReason
                FROM [dbo].[Stores] stores 
                LEFT JOIN [dbo].[Nodes] nodes 
                    ON stores.Id = nodes.StoreId
                WHERE stores.Id = @StoreId";

            var lookup = new Dictionary<Guid, StoreReadModel>();
            var stores = await _connection.QueryAsync<StoreReadModel, NodeReadModel, StoreReadModel>(query, (store, node) =>
            {
                StoreReadModel readModel;
                if (!lookup.TryGetValue(store.Id, out readModel))
                {
                    readModel = store;
                    lookup.Add(readModel.Id, readModel);
                }
                readModel.Nodes = readModel.Nodes ?? new List<NodeReadModel>();
                if (node != null)
                {
                    readModel.Nodes.Add(node);
                }
                return readModel;
            },
            new { StoreId = id });
            return stores.FirstOrDefault();
        }
    }
}