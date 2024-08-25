using SalesDataPredictionAPI.Models;

namespace SalesDataPredictionAPI.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
    }
}