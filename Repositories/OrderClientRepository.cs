using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using SalesDataPredictionAPI.Data;
using SalesDataPredictionAPI.Models;
using SalesDataPredictionAPI.Repositories.Interfaces;

namespace SalesDataPredictionAPI.Repositories
{
    public class OrderClientRepository : IOrderClientRepository
    {
        private readonly DbConnectionFactory _connectionFactory;

        public OrderClientRepository(DbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<OrderClient>> GetOrdersClientAsync()
        {
            try
            {
                using (IDbConnection db = _connectionFactory.CreateConnection())
                {
                    return await db.QueryAsync<OrderClient>("SELECT * FROM Sales.ViewGetClientOrders");
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

        public async Task<OrderClient?> GetOrdersClientByIdAsync(int id)
        {
            try
            {
                using (IDbConnection db = _connectionFactory.CreateConnection())
                {
                    return await db.QueryFirstOrDefaultAsync<OrderClient>("SELECT * FROM Sales.ViewGetClientOrders WHERE OrderId = @OrderId", new { OrderId = id });
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

        public async Task<int> AddOrderClientAsync(NewOrderClient newOrderClient)
        {
            using (IDbConnection db = _connectionFactory.CreateConnection())
            {
                var storedProcedure = "sp_CreateOrderWithDetails";

                var parameters = new DynamicParameters();
                parameters.Add("@CustId", newOrderClient.CustId);
                parameters.Add("@EmpId", newOrderClient.EmpId);
                parameters.Add("@ShipperId", newOrderClient.ShipperId);
                parameters.Add("@ShipName", newOrderClient.ShipName);
                parameters.Add("@ShipAddress", newOrderClient.ShipAddress);
                parameters.Add("@ShipCity", newOrderClient.ShipCity);
                parameters.Add("@OrderDate", newOrderClient.OrderDate);
                parameters.Add("@RequiredDate", newOrderClient.RequiredDate);
                parameters.Add("@Shippeddate", newOrderClient.ShippedDate);
                parameters.Add("@ShipCountry", newOrderClient.ShipCountry);
                parameters.Add("@Freight", newOrderClient.Freight);
                parameters.Add("@ProductId", newOrderClient.ProductId);
                parameters.Add("@UnitPrice", newOrderClient.UnitPrice);
                parameters.Add("@Qty", newOrderClient.Qty);
                parameters.Add("@Discount", newOrderClient.Discount);
                parameters.Add("@NewOrderID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                try
                {
                    await db.QuerySingleOrDefaultAsync<int>(
                        storedProcedure,
                        parameters,
                        commandType: CommandType.StoredProcedure);

                    int newOrderId = parameters.Get<int>("@NewOrderID");

                    return newOrderId;
                }
                catch (SqlException ex)
                {
                    throw new DataAccessException("Error accessing the database", ex);
                }
                catch (TimeoutException ex)
                {
                    throw new DataAccessException("Database operation timed out", ex);
                }
                catch (Exception ex)
                {
                    throw new DataAccessException("An error occurred while retrieving the order", ex);
                }
            }
        }
    }
}