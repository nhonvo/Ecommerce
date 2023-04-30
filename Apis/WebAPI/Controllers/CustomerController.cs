using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Customer;
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
        [HttpGet("Id")]
        public async Task<ApiResult<CustomerResponse>> Get(string Id)
            => await _customerService.GetByIdAsync(Id);
        [HttpGet]
        public async Task<ApiResult<Pagination<CustomerResponse>>> Get(int pageIndex = 0, int pageSize = 10)
            => await _customerService.GetAsync(pageIndex, pageSize);
        [HttpPost]
        public async Task<ApiResult<CustomerResponse>> Post([FromBody] CreateCustomer request)
            => await _customerService.AddAsync(request);
        [HttpPut]
        public async Task<ApiResult<CustomerResponse>> Put(UpdateCustomer request)
            => await _customerService.Update(request);
        [HttpDelete]
        public async Task<ApiResult<CustomerResponse>> Delete(string id)
            => await _customerService.Delete(id);
    }
}