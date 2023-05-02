using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Order;
using Application.ViewModels.OrderDetails;
using Domain.Aggregate.AppResult;
using Microsoft.AspNetCore.Mvc;
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
            var response = await _orderService.GetAsync(id);
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
        public async Task<ActionResult<ApiResult<OrderResponse>>> Delete(Guid id)
        {
            var response = await _orderService.Delete(id);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest();
            return Ok(response);
        }
        [HttpGet("{id}/orderItems")]
        public async Task<ActionResult<ApiResult<Pagination<OrderResponse>>>> GetOrder(Guid id, int pageIndex = 0, int pageSize = 10)
        {
            var response = await _orderService.GetOrder(id, pageIndex, pageSize);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest();
            return Ok(response);
        }

        [HttpPost("{id}/orderItems")]
        public async Task<ActionResult<ApiResult<OrderResponse>>> PostOrder(Guid id, AddOrderDetail request)
        {
            var response = await _orderService.AddOrder(id, request);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest();
            return Ok(response);
        }

        [HttpPut("{id}/orderItems")]
        public async Task<ActionResult<ApiResult<OrderResponse>>> PutOrder(Guid id, UpdateOrderDetail request)
        {
            var response = await _orderService.UpdateOrder(id, request);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest();
            return Ok(response);
        }

        [HttpDelete("{id}/orderItems")]
        public async Task<ActionResult<ApiResult<OrderResponse>>> DeleteOrder(Guid id)
        {
            var response = await _orderService.DeleteOrder(id);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest();
            return Ok(response);
        }
        [HttpGet("Search")]
        public async Task<ActionResult<ApiResult<Pagination<OrderResponse>>>> Search(string name, int pageIndex = 0, int pageSize = 10)
        {
            var response = await _orderService.Search(name, pageIndex, pageSize);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest();
            return Ok(response);
        }
    }
}