using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomersProvider:ICustomersProvider
    {
        public CustomersDbContext dbContext { get; }
        public ILogger<CustomersProvider> Logger { get; }
        public IMapper Mapper { get; }

        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.Logger = logger;
            this.Mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Db.Customer() { Id = 1, Name = "Karthi", Address = "F2,Cardiff" });
                dbContext.Customers.Add(new Db.Customer() { Id = 2, Name = "Devaki", Address = "F2,Cardiff" });
                dbContext.Customers.Add(new Db.Customer() { Id = 3, Name = "Pugazh", Address = "F2,Cardiff" });
                dbContext.Customers.Add(new Db.Customer() { Id = 4, Name = "Harsith", Address = "F2,Cardiff" });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.Customer>, string Error)> GetCustomersAsync()
        {
            try
            {
                var Customers = await this.dbContext.Customers.ToListAsync();

                if (Customers.Any())
                {
                    IEnumerable<Models.Customer> result = this.Mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(Customers).ToList();
                    return (true, result, "");
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool isSuccess, Models.Customer, string Error)> GetCustomerAsync(int Id)
        {
            try
            {
                var Customers = await this.dbContext.Customers.FirstOrDefaultAsync(t => t.Id == Id);

                if (Customers != null)
                {
                    var result = this.Mapper.Map<Db.Customer, Models.Customer>(Customers);
                    return (true, result, "");
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
