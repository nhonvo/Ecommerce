using Application.Interfaces;
using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Customer;
using Domain.Aggregate.AppResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.ViewModels.Order;

namespace WebAPI.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("Id")]
        public async Task<ApiResult<OrderResponse>> Get(string Id)
            => await _orderService.GetByIdAsync(Id);
        [HttpGet]
        public async Task<ApiResult<Pagination<OrderResponse>>> Get(int pageIndex = 0, int pageSize = 10)
            => await _orderService.GetAsync(pageIndex, pageSize);
        [HttpPost]
        public async Task<ApiResult<OrderResponse>> Post([FromBody] CreateOrder request)
            => await _orderService.AddAsync(request);
        [HttpPut]
        public async Task<ApiResult<OrderResponse>> Put(UpdateOrder request)
            => await _orderService.Update(request);
        [HttpDelete]
        public async Task<ApiResult<OrderResponse>> Delete(string id)
            => await _orderService.Delete(id);
    }
}