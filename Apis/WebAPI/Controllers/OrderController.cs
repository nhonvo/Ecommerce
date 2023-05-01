using Application.Interfaces;
using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Customer;
using Domain.Aggregate.AppResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.ViewModels.Order;
using System.Net;

namespace WebAPI.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<OrderResponse>>> Get(Guid id)
        {
            var response = await _orderService.Get(id);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest();
            return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult<ApiResult<Pagination<OrderResponse>>>> Get(int pageIndex = 0, int pageSize = 10)
        {
            var response = await _orderService.GetAsync(pageIndex, pageSize);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<OrderResponse>>> Post([FromBody] CreateOrder request)
        {
            var response = await _orderService.AddAsync(request);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest();
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResult<OrderResponse>>> Put(UpdateOrder request)
        {
            var response = await _orderService.Update(request);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest();
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<ApiResult<OrderResponse>>> Delete(string id)
        {
            var response = await _orderService.Delete(id);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest();
            return Ok(response);
        }

    }
}