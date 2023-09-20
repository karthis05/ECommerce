using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
    public class OrderService : IOrderService
    {
        public IHttpClientFactory HttpClientFactory { get; }
        public ILogger<OrderService> Logger { get; }

        public OrderService(IHttpClientFactory httpClientFactory, ILogger<OrderService> logger)
        {
            HttpClientFactory = httpClientFactory;
            Logger = logger;
        }

       

        public async Task<(bool isSuccess, IEnumerable<Order>, string ErrorMessgae)> GetOrderAsync(int customerId)
        {
            var client= HttpClientFactory.CreateClient("OrdersService");

            var response = await client.GetAsync($"api/orders/{customerId}");
            if(response.IsSuccessStatusCode) 
            {
                var content = await response.Content.ReadAsByteArrayAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var jsonresult = JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);
                
                return(true, jsonresult, "");
            }
            return (false, null, "Not Found");
        }
    }
}
