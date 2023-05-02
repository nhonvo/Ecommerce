using Application;
using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Product;
using AutoMapper;
using Domain.Aggregate.AppResult;
using Domain.Entities;
namespace Infrastructures.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResult<Pagination<ProductResponse>>> GetAsync(int pageIndex, int pageSize)
        {
            var result = await _unitOfWork.ProductRepository.ToPagination(pageIndex, pageSize);
            var products = _mapper.Map<Pagination<ProductResponse>>(result);
            if (products == null)
                return new ApiErrorResult<Pagination<ProductResponse>>("Can't get product");
            return new ApiSuccessResult<Pagination<ProductResponse>>(products);
        }
        public async Task<ApiResult<ProductResponse>> AddAsync(CreateProduct request)
        {
            var product = _mapper.Map<Product>(request);
            try
            {
                await _unitOfWork.ExecuteTransactionAsync(() => _unitOfWork.ProductRepository.AddAsync(product));

                var result = _mapper.Map<ProductResponse>(product);

                return new ApiSuccessResult<ProductResponse>(result);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<ProductResponse>("Can't add product", new List<string> { ex.ToString() });
            }
        }
        public async Task<ApiResult<ProductResponse>> Update(UpdateProduct request)
        {
            var productExist = await _unitOfWork.ProductRepository.FirstOrDefaultAsync(x => x.Id == request.Id);
            /// Product is not found.
            if (productExist == null)
                return new ApiErrorResult<ProductResponse>("Product not found");
            var product = _mapper.Map<Product>(request);

            try
            {
                await _unitOfWork.ExecuteTransactionAsync(() =>
                {
                    _unitOfWork.ProductRepository.Update(product);
                });
                var result = _mapper.Map<ProductResponse>(product);
                return new ApiSuccessResult<ProductResponse>(result);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<ProductResponse>("Can't update product", new List<string> { ex.ToString() });
            }
        }
        public async Task<ApiResult<ProductResponse>> Delete(string Id)
        {
            var product = await _unitOfWork.ProductRepository.FirstOrDefaultAsync(x => x.Id.ToString() == Id);
            /// Return a ProductResponse object if product is null
            if (product == null)
                return new ApiErrorResult<ProductResponse>("Product not found");
            try
            {
                await _unitOfWork.ExecuteTransactionAsync(() => _unitOfWork.ProductRepository.Delete(product));

                var result = _mapper.Map<ProductResponse>(product);
                return new ApiSuccessResult<ProductResponse>(result);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<ProductResponse>("Can't delete product", new List<string> { ex.ToString() });
            }
        }
        public async Task<ApiResult<ProductResponse>> Get(Guid Id)
        {
            var product = await _unitOfWork.ProductRepository.FirstOrDefaultAsync(x => x.Id == Id);
            var result = _mapper.Map<ProductResponse>(product);
            /// Returns the result of the product.
            if (result == null)
                return new ApiErrorResult<ProductResponse>("Not found the product");
            return new ApiSuccessResult<ProductResponse>(result);
        }
        public async Task<ApiResult<Pagination<ProductResponse>>> Search(string search, int pageIndex, int pageSize)
        {
            var products = await _unitOfWork.ProductRepository.GetAsync(
                filter: x => x.Name.Contains(search)
                             || x.Description.Contains(search)
                             || x.Price.ToString().Contains(search),
                pageIndex: pageIndex,
                pageSize: pageSize
            );
            var result = _mapper.Map<Pagination<ProductResponse>>(products);
            if (result == null)
                return new ApiErrorResult<Pagination<ProductResponse>>("Can't get product");
            return new ApiSuccessResult<Pagination<ProductResponse>>(result);
        }
    }
}
