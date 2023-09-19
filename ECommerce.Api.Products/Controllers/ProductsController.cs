using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : Controller
    {
        public IProductsProvider ProductsProvider { get; }

        public ProductsController(IProductsProvider productsProvider)
        {
            ProductsProvider = productsProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var result = await this.ProductsProvider.GetProductsASync();

            if (result.isSuccess)
            {
                return Ok(result.Products);
            }

            return NotFound();
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProductsASync(int Id)
        {
            var result = await this.ProductsProvider.GetProductASync(Id);

            if (result.isSuccess)
            {
                return Ok(result.Product);
            }

            return NotFound();
        }
    }
}
