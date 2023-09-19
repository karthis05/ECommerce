using ECommerce.Api.Products.Models;

namespace ECommerce.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        Task<(bool isSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsASync();
        Task<(bool isSuccess, Models.Product Product, string ErrorMessage)> GetProductASync(int Id);
    }
}
