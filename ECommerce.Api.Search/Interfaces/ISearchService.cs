namespace ECommerce.Api.Search.Interfaces
{
    public interface ISearchService
    {
        Task<(bool isSuccess, dynamic SearchResult)> SearchReasultAsync(int CustomerId);
    }
}
