using SalesDataPredictionAPI.Models;

namespace SalesDataPredictionAPI.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}