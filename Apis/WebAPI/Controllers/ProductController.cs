using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Product;
using Domain.Aggregate.AppResult;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<ProductResponse>>> Get(Guid id)
        {
            var response = await _productService.Get(id);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest(response);
            return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult<ApiResult<Pagination<ProductResponse>>>> Get(int pageIndex = 0, int pageSize = 10)
        {
            var response = await _productService.GetAsync(pageIndex, pageSize);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest(response);
            return Ok(response);
        }
        [HttpGet("Search")]
        public async Task<ActionResult<ApiResult<Pagination<ProductResponse>>>> Search(string name, int pageIndex = 0, int pageSize = 10)
        {
            var response = await _productService.Search(name, pageIndex, pageSize);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<ProductResponse>>> Post([FromBody] CreateProduct request)
        {
            var response = await _productService.AddAsync(request);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResult<ProductResponse>>> Put(UpdateProduct request)
        {
            var response = await _productService.Update(request);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<ApiResult<ProductResponse>>> Delete(string id)
        {
            var response = await _productService.Delete(id);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest(response);
            return Ok(response);
        }
        [HttpGet("top-sellers/start={start}&end={end}")]
        public async Task<ActionResult<Pagination<TopSellingProduct>>> GetTopSellingProducts(
           DateTime start,
           DateTime end,
           int pageIndex = 0,
           int pageSize = 10)
        {
            var response = await _productService.GetTopSellingProducts(start, end, pageIndex, pageSize);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest(response);
            return Ok(response);
        }
    }
}