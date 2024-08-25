using Microsoft.AspNetCore.Mvc;
using SalesDataPredictionAPI.Models;
using SalesDataPredictionAPI.Repositories.Interfaces;

namespace SalesDataPredictionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
        {
            var customers = await _customerRepository.GetCustomerAsync();

            return Ok(customers);
        }
    }
}