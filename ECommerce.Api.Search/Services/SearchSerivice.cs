using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Services
{
    public class SearchSerivice : ISearchService
    {


        public SearchSerivice(IOrderService orderService, IProductsService productsService)
        {
            OrderService = orderService;
            ProductsService = productsService;
        }

        public IOrderService OrderService { get; }
        public IProductsService ProductsService { get; }

        public async Task<(bool isSuccess, dynamic SearchResult)> SearchReasultAsync(int CustomerId)
        {
            var orderresult = await OrderService.GetOrderAsync(CustomerId);
            var productresult = await ProductsService.GetProductsAsync();
            if (orderresult.isSuccess)
            {
                foreach (var order in orderresult.Item2)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productresult.isSuccess? productresult.Item2.FirstOrDefault(t => t.Id == item.ProductId).Name:"No data found";
                    }
                }

                var result = new
                {
                    Orders = orderresult.Item2
                };

                return (true, result);
            }

            return (false,null);
        }
    }
}
