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
                List<Db.OrderItem> OrderItems = new List<Db.OrderItem>();
                OrderItems.Add(new Db.OrderItem { Id = 1, OrderId = 1, ProductId = 1, Quantity = 10, UnitPrice = 300 });
                OrderItems.Add(new Db.OrderItem { Id = 2, OrderId = 2, ProductId = 2, Quantity = 101, UnitPrice = 200 });
                OrderItems.Add(new Db.OrderItem { Id = 3, OrderId = 3, ProductId = 3, Quantity = 102, UnitPrice = 350 });
                OrderItems.Add(new Db.OrderItem { Id = 4, OrderId = 4, ProductId = 4, Quantity = 104, UnitPrice = 350 });

                DbContext.Order.Add(new Db.Order { Id = 1, CustomerId = 1, OrderDate = DateTime.Now.AddDays(-10).Date, Total = 25, Items = OrderItems });
                DbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.Order>, string Error)> GetOrdersAsync(int customerId)
        {
            try
            {
                var result = await DbContext.Order.Where(t => t.CustomerId == customerId).ToListAsync();

                if (result != null && result.Count>0)
                {
                    var orders = Mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(result);

                    return (true, orders, "");
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex) 
            {
                Logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
       
    }
}
