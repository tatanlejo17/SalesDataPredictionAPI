using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using SalesDataPredictionAPI.Data;
using SalesDataPredictionAPI.Models;
using SalesDataPredictionAPI.Repositories.Interfaces;

namespace SalesDataPredictionAPI.Repositories
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public ShipperRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Shipper>> GetShipperAsync()
        {
            try
            {
                using (IDbConnection db = _connectionFactory.CreateConnection())
                {
                    return await db.QueryAsync<Shipper>("SELECT * FROM Sales.ViewGetShippers");
                }
            }
            catch (SqlException ex)
            {
                throw new DataAccessException("Error accessing the database", ex);
            }
            catch (TimeoutException ex)
            {
                // Log the timeout exception
                throw new DataAccessException("Database operation timed out", ex);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                throw new DataAccessException("An error occurred while retrieving the order", ex);
            }
        }
    }
}