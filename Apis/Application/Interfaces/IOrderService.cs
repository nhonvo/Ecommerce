using Application;
using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Order;
using Application.ViewModels.OrderDetails;
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
        Task<ApiResult<OrderResponse>> GetAsync(Guid Id);
        Task<ApiResult<Pagination<OrderResponse>>> GetAsync(int pageIndex, int pageSize);
        Task<ApiResult<OrderResponse>> AddAsync(CreateOrder request);
        Task<ApiResult<OrderResponse>> Update(UpdateOrder request);
        Task<ApiResult<OrderResponse>> Delete(Guid Id);
        Task<ApiResult<Pagination<OrderResponse>>> GetOrder(Guid Id, int pageIndex, int pageSize);
        Task<ApiResult<OrderResponse>> AddOrder(Guid Id, AddOrderDetail request);
        Task<ApiResult<OrderResponse>> UpdateOrder(Guid Id, UpdateOrderDetail request);
        Task<ApiResult<OrderResponse>> DeleteOrder(Guid Id);
        Task<ApiResult<Pagination<OrderResponse>>> Search(string search, int pageIndex, int pageSize);
    }
}
