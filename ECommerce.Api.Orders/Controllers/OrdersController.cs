using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : Controller
    {
        public OrdersController(IOrdersProvider ordersProvider)
        {
            OrdersProvider = ordersProvider;
        }

        public IOrdersProvider OrdersProvider { get; }

        [HttpGet("{customerid}")]
        public async Task<IActionResult> GetOrdersAsync(int customerid)
        {
            var result = await OrdersProvider.GetOrdersAsync(customerid);

            if(result.isSuccess) 
            { 
                return Ok(result.Item2); 
            }

            return NotFound();
        }
    }
}
