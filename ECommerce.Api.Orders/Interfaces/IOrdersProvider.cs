namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool isSuccess, IEnumerable<Models.Order>, string Error)> GetOrdersAsync(int customerId);

    }
}
