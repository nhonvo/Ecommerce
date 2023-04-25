using Application.Commons;
using Application.ViewModels.Book;
using Domain.Aggregate.AppResult;

namespace Application.Interfaces
{
    public interface IBookService
    {
        /// <summary>
        /// Gets information about a book. You can use this method to retrieve more than one page of books at once.
        /// </summary>
        /// <param name="pageIndex">The zero - based index of the page to retrieve.</param>
        /// <param name="pageSize">The size of the page to retrieve. Must be greater than or equal</param>
        Task<ApiResult<Pagination<BookResponse>>> GetAsync(int pageIndex, int pageSize);
        /// <summary>
        /// Searches for books matching the query. This is the same as the search method but the results are paged to a maximum of 10 results
        /// </summary>
        /// <param name="query">The query to search for</param>
        /// <param name="pageIndex">The page index to start the search at</param>
        /// <param name="pageSize">The size of the page to be returned. Default is</param>
        Task<ApiResult<Pagination<BookResponse>>> Search(string query, int pageIndex = 0, int pageSize = 10);
        /// <summary>
        /// Searches for books by the Advanced Search. You can use this method to perform advanced search without having to know the full path to the book or a search engine that is capable of returning large amounts of results.
        /// </summary>
        /// <param name="request">The request containing the search parameters. See for more information</param>
        /// <param name="pageIndex">The page index of the search results to return.</param>
        /// <param name="pageSize">The size of the page of results to return</param>
        Task<ApiResult<Pagination<BookResponse>>> AdvancedSearch(SearchRequest request, int pageIndex, int pageSize);
        /// <summary>
        /// Get a book by id. This is the equivalent of GET / books / { id }
        /// </summary>
        /// <param name="id">The id of the book to retrieve. Use null to retrieve</param>
        Task<ApiResult<BookResponse>> GetByIdAsync(string id);
        /// <summary>
        /// Add a book to the user's book list. This is a blocking call and returns immediately
        /// </summary>
        /// <param name="request">Information about the book to</param>
        Task<ApiResult<BookResponse>> AddAsync(CreateBook request);
        /// <summary>
        /// Update a book in TM1. You can use this method to update an existing book in TM1
        /// </summary>
        /// <param name="request">Information about the book to</param>
        Task<ApiResult<BookResponse>> Update(UpdateBook request);
        /// Deletes the book from the database and returns the result. This is a no - op
        Task<ApiResult<BookResponse>> Delete(string Id);
    }
}
