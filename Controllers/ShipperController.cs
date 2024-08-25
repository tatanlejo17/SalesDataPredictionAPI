using Microsoft.AspNetCore.Mvc;
using SalesDataPredictionAPI.Models;
using SalesDataPredictionAPI.Repositories.Interfaces;

namespace SalesDataPredictionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ShipperController : ControllerBase
    {
        private readonly IShipperRepository _shipperRepository;

        public ShipperController(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shipper>>> GetShipper()
        {
            var shipper = await _shipperRepository.GetShipperAsync();

            return Ok(shipper);
        }
    }
}