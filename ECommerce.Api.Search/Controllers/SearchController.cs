using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Search.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : Controller
    {
        public ISearchService SearchService { get; }



        public SearchController(ISearchService searchService)
        {
            SearchService = searchService;
        }
        [HttpPost]
        public async Task<IActionResult> SearchAsync(SearchTerm term)
        {
            var result = await SearchService.SearchReasultAsync(term.CustomerId);

            if(result.isSuccess)
            {
                return Ok(result.SearchResult);
            }
            return NotFound();
        }
    }
}
