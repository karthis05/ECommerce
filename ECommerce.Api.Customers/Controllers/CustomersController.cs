using ECommerce.Api.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : Controller
    {
        public ICustomersProvider CustomersProvider { get; }

        public CustomersController(ICustomersProvider customersProvider)
        {
            this.CustomersProvider = customersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var result = await CustomersProvider.GetCustomersAsync();

            if(result.isSuccess)
            {
                return Ok(result.Item2);
            }
            return NotFound();
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCustomerAsync(int Id)
        {
            var result = await CustomersProvider.GetCustomerAsync(Id);

            if (result.isSuccess)
            {
                return Ok(result.Item2);
            }
            return NotFound();
        }
    }
}
