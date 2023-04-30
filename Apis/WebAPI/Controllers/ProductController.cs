using Application.Interfaces;
using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Customer;
using Domain.Aggregate.AppResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.ViewModels.Order;
using Application.ViewModels.Product;

namespace WebAPI.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("Id")]
        public async Task<ApiResult<ProductResponse>> Get(string Id)
          => await _productService.GetByIdAsync(Id);
        [HttpGet]
        public async Task<ApiResult<Pagination<ProductResponse>>> Get(int pageIndex = 0, int pageSize = 10)
            => await _productService.GetAsync(pageIndex, pageSize);
        [HttpPost]
        public async Task<ApiResult<ProductResponse>> Post([FromBody] CreateProduct request)
            => await _productService.AddAsync(request);
        [HttpPut]
        public async Task<ApiResult<ProductResponse>> Put(UpdateProduct request)
            => await _productService.Update(request);
        [HttpDelete]
        public async Task<ApiResult<ProductResponse>> Delete(string id)
            => await _productService.Delete(id);
    }
}