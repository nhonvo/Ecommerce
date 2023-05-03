using Application.Commons;
using Application.ViewModels.Product;
using Domain.Aggregate.AppResult;
namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<ApiResult<Pagination<ProductResponse>>> GetAsync(int pageIndex, int pageSize);
        Task<ApiResult<ProductResponse>> AddAsync(CreateProduct request);
        Task<ApiResult<ProductResponse>> Update(UpdateProduct request);
        Task<ApiResult<ProductResponse>> Delete(string Id);
        Task<ApiResult<ProductResponse>> Get(Guid Id);
        Task<ApiResult<Pagination<ProductResponse>>> Search(string search, int pageIndex, int pageSize);
        Task<ApiResult<Pagination<TopSellingProduct>>> GetTopSellingProducts(
            DateTime start,
            DateTime end,
            int pageIndex = 0,
            int pageSize = 10);
    }
}
