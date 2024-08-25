using SalesDataPredictionAPI.Models;

namespace SalesDataPredictionAPI.Repositories.Interfaces
{
    public interface IOrderClientRepository
    {
        Task<IEnumerable<OrderClient>> GetOrdersClientAsync();
        Task<OrderClient?> GetOrdersClientByIdAsync(int id);
        Task<int> AddOrderClientAsync(NewOrderClient newOrderClient);
    }
}