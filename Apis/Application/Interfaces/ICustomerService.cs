using Application;
using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Customer;
using Application.ViewModels.Order;
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
        Task<ApiResult<Pagination<CustomerResponse>>> GetOrder(Guid id, int pageIndex, int pageSize);
        Task<ApiResult<OrderResponse>> AddOrder(Guid id);
        Task<ApiResult<OrderResponse>> UpdateOrder(Guid id, Guid orderId, UpdateCustomerOrder request);
        Task<ApiResult<CustomerResponse>> AddAsync(CreateCustomer request);
        Task<ApiResult<CustomerResponse>> Update(UpdateCustomer request);
        Task<ApiResult<CustomerResponse>> Delete(string Id);
        Task<ApiResult<CustomerResponse>> Get(Guid Id);

    }
}
