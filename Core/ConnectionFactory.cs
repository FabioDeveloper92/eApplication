using System;
using System.Data;
using System.Data.SqlClient;
using Config;

namespace Infrastructure.Core
{
    public interface IConnectionFactory : IDisposable
    {
        IDbConnection Connection { get; }
        IUnitOfWork BeginUnitOfWork();
    }

    public class ConnectionFactory : IConnectionFactory
    {
        private IDbConnection _connection;

        private readonly SqlConnectionString _sqlConnectionString;

        public ConnectionFactory(SqlConnectionString sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }

        public IDbConnection Connection
        {
            get
            {
                if (_connection != null) return _connection;

                _connection = new SqlConnection(_sqlConnectionString.Value);
                _connection.Open();

                return _connection;
            }
        }

        public virtual IUnitOfWork BeginUnitOfWork()
        {
            return new UnitOfWork(Connection);
        }

        public void Dispose()
        {
            if (_connection == null) return;

            _connection.Close();
            _connection.Dispose();
        }

    }
}
