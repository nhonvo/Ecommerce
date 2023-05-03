using Application.Commons;
using Application.ViewModels.Order;
using Application.ViewModels.OrderDetails;
using Application.ViewModels.Product;
using Domain.Aggregate.AppResult;
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
        Task<ApiResult<OrderDetails>> DeleteOrder(Guid orderId, Guid productId);
        Task<ApiResult<Pagination<OrderResponse>>> Search(
            string search,
            int pageIndex,
            int pageSize);
      
        Task<ApiResult<decimal>> GetAverageOrderValue(DateTime start, DateTime end);
    }
}
