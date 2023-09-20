using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System.Net.Http;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
    public class ProductsService : IProductsService
    {
        public IHttpClientFactory HttpClientFactory { get; }
        public ILogger<ProductsService> Logger { get; }

        public ProductsService(IHttpClientFactory httpClientFactory, ILogger<ProductsService> logger)
        {
            HttpClientFactory = httpClientFactory;
            Logger = logger;
        }        

        public async Task<(bool isSuccess, IEnumerable<Product>, string ErrorMessage)> GetProductsAsync()
        {
            var client = HttpClientFactory.CreateClient("ProductsService");
            var response = await client.GetAsync($"api/products");

            if (response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsByteArrayAsync();
                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, options);

                return (true, result,"");
            }
            return (false, null, response.ReasonPhrase);
        }
    }
}
