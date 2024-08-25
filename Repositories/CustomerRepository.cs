using System.Data;
using SalesDataPredictionAPI.Data;
using SalesDataPredictionAPI.Models;
using SalesDataPredictionAPI.Repositories.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;

namespace SalesDataPredictionAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        // Constructor
        public CustomerRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Customer>> GetCustomerAsync()
        {
            try
            {
                using (IDbConnection db = _connectionFactory.CreateConnection())
                {
                    return await db.QueryAsync<Customer>("SELECT * FROM Sales.ViewNextOrderPrediction");
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