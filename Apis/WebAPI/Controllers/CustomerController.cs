using System.Net;
using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Customer;
using Application.ViewModels.Order;
using Domain.Aggregate.AppResult;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResult<CustomerResponse>>> Get(Guid id)
        {
            var response = await _customerService.GetAsync(id);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest();
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResult<Pagination<CustomerResponse>>>> Get(int pageIndex = 0, int pageSize = 10)
        {
            var response = await _customerService.GetAsync(pageIndex, pageSize);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest();
            return Ok(response);
        }
        [HttpGet("{id}/Order")]
        public async Task<ActionResult<ApiResult<Pagination<CustomerResponse>>>> GetOrder(Guid id, int pageIndex = 0, int pageSize = 10)
        {
            var response = await _customerService.GetOrder(id, pageIndex, pageSize);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest();
            return Ok(response);
        }
        [HttpPost("{id}/Order")]
        public async Task<ActionResult<ApiResult<CustomerResponse>>> AddOrder(Guid id)
        {
            var response = await _customerService.AddOrder(id);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest();
            return Ok(response);
        }
        [HttpPut("{id}/Order/{orderId}")]
        public async Task<ActionResult<ApiResult<CustomerResponse>>> PostOrder(Guid id, Guid orderId, UpdateCustomerOrder request)
        {
            var response = await _customerService.UpdateOrder(id, orderId, request);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest();
            return Ok(response);
        }
        [HttpDelete("{id}/Order/{orderId}")]
        public async Task<ActionResult<ApiResult<CustomerResponse>>> DeleteOrder(Guid id, Guid orderId)
        {
            var response = await _customerService.DeleteOrder(id, orderId);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResult<CustomerResponse>>> Post([FromBody] CreateCustomer request)
        {
            var response = await _customerService.AddAsync(request);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest();
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResult<CustomerResponse>>> Put(UpdateCustomer request)
        {
            var response = await _customerService.Update(request);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest();
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<ApiResult<CustomerResponse>>> Delete(string id)
        {
            var response = await _customerService.Delete(id);
            if (response.StatusCode != HttpStatusCode.OK && response.ResultObject == null)
                return BadRequest();
            return Ok(response);
        }

    }
}