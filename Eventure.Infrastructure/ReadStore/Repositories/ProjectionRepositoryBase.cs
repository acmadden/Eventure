using System;
using System.Data;
using System.Data.SqlClient;
using Eventure.Infrastructure.ReadStore.Settings;
using Microsoft.Extensions.Options;

namespace Eventure.Infrastructure.ReadStore.Repositories
{
    public abstract class ProjectionRepositoryBase : IDisposable
    {
        private bool _disposed;
        protected readonly IDbConnection _connection;

        public ProjectionRepositoryBase(IOptions<SqlServerSettings> options)
        {
            _connection = new SqlConnection(options.Value.ConnectionString);
            _disposed = false;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_disposed)
                {
                    if (_connection != null)
                    {
                        _connection.Close();
                        _connection.Dispose();
                    }
                    _disposed = true;
                }
            }
        }
    }
}