using Application;
using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Order;
using AutoMapper;
using Domain.Aggregate;
using Domain.Aggregate.AppResult;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Application.Interfaces
{
    public interface IOrderService
    {
        Task<ApiResult<Pagination<OrderResponse>>> GetAsync(int pageIndex, int pageSize);
        Task<ApiResult<OrderResponse>> AddAsync(CreateOrder request);
        Task<ApiResult<OrderResponse>> Update(UpdateOrder request);
        Task<ApiResult<OrderResponse>> Delete(string Id);
        Task<ApiResult<OrderResponse>> GetByIdAsync(string Id);
    }
}
