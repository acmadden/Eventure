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
            string query = @"SELECT * FROM [dbo].[Stores] stores WHERE stores.Id = @StoreId";
            return await _connection.QueryFirstOrDefaultAsync<StoreReadModel>(query, new { StoreId = id });
        }
    }
}