using Microsoft.AspNetCore.Mvc;
using SalesDataPredictionAPI.Models;
using SalesDataPredictionAPI.Repositories.Interfaces;

namespace SalesDataPredictionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderClientController : ControllerBase
    {
        private readonly IOrderClientRepository _orderClientRepository;

        public OrderClientController(IOrderClientRepository orderClientRepository)
        {
            _orderClientRepository = orderClientRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderClient>>> GetOrdersClient()
        {
            var orderClient = await _orderClientRepository.GetOrdersClientAsync();

            return Ok(orderClient);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderClient>> GetOrdersClientById(int id)
        {
            var orderClient = await _orderClientRepository.GetOrdersClientByIdAsync(id);

            if (orderClient == null)
            {
                return NotFound();
            }

            return Ok(orderClient);
        }

        [HttpPost]
        public async Task<ActionResult<OrderClient>> Post([FromBody] NewOrderClient newOrderClient)
        {
            var objOrderClient = new NewOrderClient
            {
                CustId = newOrderClient.CustId,
                EmpId = newOrderClient.EmpId,
                ShipperId = newOrderClient.ShipperId,
                ShipName = newOrderClient.ShipName,
                ShipAddress = newOrderClient.ShipAddress,
                ShipCity = newOrderClient.ShipCity,
                OrderDate = newOrderClient.OrderDate,
                RequiredDate = newOrderClient.RequiredDate,
                ShippedDate = newOrderClient.ShippedDate,
                ShipCountry = newOrderClient.ShipCountry,
                Freight = newOrderClient.Freight,
                ProductId = newOrderClient.ProductId,
                UnitPrice = newOrderClient.UnitPrice,
                Qty = newOrderClient.Qty,
                Discount = newOrderClient.Discount
            };

            var resOrderId = await _orderClientRepository.AddOrderClientAsync(objOrderClient);
            Console.WriteLine("res: {0}", resOrderId);

            var response = new OrderClientCreatedResponse
            {
                OrderId = resOrderId,
                newOrderClient = objOrderClient
            };

            return CreatedAtAction(nameof(GetOrdersClientById), new { id = resOrderId }, response);
        }
    }
}