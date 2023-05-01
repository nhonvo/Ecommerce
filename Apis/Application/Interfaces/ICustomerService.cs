using Application;
using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Customer;
using AutoMapper;
using Domain.Aggregate;
using Domain.Aggregate.AppResult;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Application.Interfaces
{
    public interface ICustomerService
    {
        Task<ApiResult<Pagination<CustomerResponse>>> GetAsync(int pageIndex, int pageSize);
        Task<ApiResult<CustomerResponse>> AddAsync(CreateCustomer request);
        Task<ApiResult<CustomerResponse>> Update(UpdateCustomer request);
        Task<ApiResult<CustomerResponse>> Delete(string Id);
        Task<ApiResult<CustomerResponse>> Get(Guid Id);

    }
}
