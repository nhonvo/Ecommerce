using Application.Commons;
using Application.ViewModels.Customer;
using Application.ViewModels.Order;
using Domain.Aggregate.AppResult;
namespace Application.Interfaces
{
    public interface ICustomerService
    {
        Task<ApiResult<Pagination<CustomerResponse>>> GetAsync(int pageIndex, int pageSize);
        Task<ApiResult<CustomerResponse>> GetAsync(Guid Id);
        Task<ApiResult<CustomerResponse>> AddAsync(CreateCustomer request);
        Task<ApiResult<CustomerResponse>> Update(UpdateCustomer request);
        Task<ApiResult<CustomerResponse>> Delete(string Id);
        Task<ApiResult<Pagination<CustomerResponse>>> GetOrder(Guid id, int pageIndex, int pageSize);
        Task<ApiResult<OrderResponse>> AddOrder(Guid id);
        Task<ApiResult<OrderResponse>> UpdateOrder(Guid id, Guid orderId, UpdateCustomerOrder request);
        Task<ApiResult<OrderResponse>> DeleteOrder(Guid id, Guid orderId);
        Task<ApiResult<Pagination<CustomerResponse>>> Search(string search, int pageIndex, int pageSize);
    }
}
