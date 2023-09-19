using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Db
{
    public class OrdersDbContext:DbContext
    {
        public DbSet<Order> Order { get; set; }

        public OrdersDbContext(DbContextOptions options) : base(options)
        {
            
        }


    }
}
