using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        public OrdersDbContext DbContext { get; }
        public ILogger<OrdersProvider> Logger { get; }
        public IMapper Mapper { get; }

        public OrdersProvider(OrdersDbContext dbContext,ILogger<OrdersProvider> looger, IMapper mapper)
        {
            DbContext = dbContext;
            Logger = looger;
            Mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!DbContext.Order.Any())
            {
                DbContext.Order.Add(new Db.Order { Id = 1, CustomerId = 1, OrderDate = DateTime.Now.AddDays(-10).Date, Total = 25, Items = new List<Db.OrderItem> { new Db.OrderItem { Id = 1, OrderId = 1, ProductId = 12, Quantity = 10, UnitPrice = 300 } } });
                DbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, Models.Order, string Error)> GetOrdersAsync(int customerId)
        {
            try
            {
                var result = await DbContext.Order.Where(t => t.CustomerId == customerId).ToListAsync();

                if (result != null && result.Count>0)
                {
                    var orders = Mapper.Map<Db.Order, Models.Order>(result[0]);

                    return (true, orders, "");
                }
                return (false, new Models.Order(), "Not Found");
            }
            catch (Exception ex) 
            {
                Logger.LogError(ex.ToString());
                return (false, new Models.Order(), ex.Message);
            }
        }
       
    }
}
