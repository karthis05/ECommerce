using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Db.Product() { Id = 1, Name = "KeyBoard", Price = 20, Inventory = 100 });
                dbContext.Products.Add(new Db.Product() { Id = 2, Name = "Mouse", Price = 10, Inventory = 70 });
                dbContext.Products.Add(new Db.Product() { Id = 3, Name = "Monitor", Price = 100, Inventory = 80 });
                dbContext.Products.Add(new Db.Product() { Id = 4, Name = "PenDrive", Price = 15, Inventory = 95 });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool isSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsASync()
        {
            try
            {
                var products = await this.dbContext.Products.ToListAsync();

                if (products.Any())
                {
                    IEnumerable<Models.Product> result = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products).ToList();
                    return (true, result, "");
                }
                return (false, null, "Not Found");
            }
            catch(Exception ex) 
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool isSuccess, Models.Product Product, string ErrorMessage)> GetProductASync(int Id)
        {
            try
            {
                var product = await this.dbContext.Products.FirstOrDefaultAsync(t => t.Id == Id);

                if (product != null)
                {
                    Models.Product result = mapper.Map<Db.Product, Models.Product>(product);
                    return (true, result, "");
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
