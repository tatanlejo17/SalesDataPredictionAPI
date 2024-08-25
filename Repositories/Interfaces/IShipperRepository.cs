using SalesDataPredictionAPI.Models;

namespace SalesDataPredictionAPI.Repositories.Interfaces
{
    public interface IShipperRepository
    {
        Task<IEnumerable<Shipper>> GetShipperAsync();
    }
}