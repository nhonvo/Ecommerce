using Application.Commons;
using Application.ViewModels.Product;
using Domain.Aggregate.AppResult;
namespace Application.Interfaces
{
    public interface IProductService
    {
        public Task<ApiResult<Pagination<ProductResponse>>> GetAsync(int pageIndex, int pageSize);
        public Task<ApiResult<ProductResponse>> AddAsync(CreateProduct request);
        public Task<ApiResult<ProductResponse>> Update(UpdateProduct request);
        public Task<ApiResult<ProductResponse>> Delete(string Id);
        public Task<ApiResult<ProductResponse>> Get(Guid Id);
        Task<ApiResult<Pagination<ProductResponse>>> Search(string search, int pageIndex, int pageSize);
    }
}
