using SalesDataPredictionAPI.Models;

namespace SalesDataPredictionAPI.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomerAsync();
    }
}