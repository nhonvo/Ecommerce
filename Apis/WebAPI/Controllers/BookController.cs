using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Book;
using Domain.Aggregate.AppResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        // [HttpGet("Id")]
        // public async Task<ApiResult<BookResponse>> Get(string Id)
        //     => await _bookService.GetByIdAsync(Id);
        // [HttpGet]
        // public async Task<ApiResult<Pagination<BookResponse>>> Search(string query, int pageIndex = 0, int pageSize = 10)
        //     => await _bookService.Search(query, pageIndex, pageSize);
        // [HttpPost]
        // public async Task<ApiResult<Pagination<BookResponse>>> AdvancedSearch(SearchRequest request, int pageIndex = 0, int pageSize = 10)
        //     => await _bookService.AdvancedSearch(request, pageIndex, pageSize);
        // [HttpGet]
        // public async Task<ApiResult<Pagination<BookResponse>>> Get(int pageIndex = 0, int pageSize = 10)
        //     => await _bookService.GetAsync(pageIndex, pageSize);
        // [HttpPost]
        // [Authorize]
        // public async Task<ApiResult<BookResponse>> Post([FromBody] CreateBook request)
        //     => await _bookService.AddAsync(request);
        // [HttpPut]
        // [Authorize]
        // public async Task<ApiResult<BookResponse>> Put(UpdateBook request)
        //     => await _bookService.Update(request);
        // [HttpDelete]
        // [Authorize]
        // public async Task<ApiResult<BookResponse>> Delete(string id)
        //     => await _bookService.Delete(id);
    }
}