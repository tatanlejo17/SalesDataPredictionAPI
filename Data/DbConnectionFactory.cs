using System.Data;
using Microsoft.Data.SqlClient;

namespace SalesDataPredictionAPI.Data
{
    public class DbConnectionFactory
    {
        private readonly string? _connectionString;

        public DbConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}