namespace ECommerce.Api.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        Task<(bool isSuccess, IEnumerable<Models.Customer>, string Error)> GetCustomersAsync();

        Task<(bool isSuccess, Models.Customer, string Error)> GetCustomerAsync(int Id);
    }
}
