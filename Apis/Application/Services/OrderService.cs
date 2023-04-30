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
namespace Infrastructures.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ApiResult<Pagination<OrderResponse>>> GetAsync(int pageIndex, int pageSize)
        {
            var result = await _unitOfWork.OrderRepository.ToPagination(pageIndex, pageSize);
            var orders = _mapper.Map<Pagination<OrderResponse>>(result);
            if (orders == null)
                return new ApiErrorResult<Pagination<OrderResponse>>("Can't get order");
            return new ApiSuccessResult<Pagination<OrderResponse>>(orders);
        }
        public async Task<ApiResult<OrderResponse>> AddAsync(CreateOrder request)
        {
            var order = _mapper.Map<Order>(request);
            try
            {
                await _unitOfWork.ExecuteTransactionAsync(() => _unitOfWork.OrderRepository.AddAsync(order));
                // _unitOfWork.BeginTransaction();
                // await _unitOfWork.OrderRepository.AddAsync(order);
                // await _unitOfWork.CommitAsync();
                var result = _mapper.Map<OrderResponse>(order);

                return new ApiSuccessResult<OrderResponse>(result);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<OrderResponse>("Can't add order", new List<string> { ex.ToString() });
            }
        }
        public async Task<ApiResult<OrderResponse>> Update(UpdateOrder request)
        {
            var orderExist = await _unitOfWork.OrderRepository.FirstOrDefaultAsync(x => x.Id == request.Id);
            /// Order is not found.
            if (orderExist == null)
                return new ApiErrorResult<OrderResponse>("Order not found");
            var order = _mapper.Map<Order>(request);

            try
            {
                _unitOfWork.BeginTransaction();
                _unitOfWork.OrderRepository.Update(order);
                await _unitOfWork.CommitAsync();
                var result = _mapper.Map<OrderResponse>(order);
                return new ApiSuccessResult<OrderResponse>(result);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<OrderResponse>("Can't update order", new List<string> { ex.ToString() });
            }
        }
        public async Task<ApiResult<OrderResponse>> Delete(string Id)
        {
            var order = await _unitOfWork.OrderRepository.FirstOrDefaultAsync(x => x.Id.ToString() == Id);
            /// Return a OrderResponse object if order is null
            if (order == null)
                return new ApiErrorResult<OrderResponse>("Order not found");
            try
            {
                await _unitOfWork.ExecuteTransactionAsync(() => _unitOfWork.OrderRepository.Delete(order));

                // _unitOfWork.BeginTransaction();
                // _unitOfWork.OrderRepository.Delete(order);
                // await _unitOfWork.CommitAsync();
                var result = _mapper.Map<OrderResponse>(order);
                return new ApiSuccessResult<OrderResponse>(result);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<OrderResponse>("Can't delete order", new List<string> { ex.ToString() });
            }
        }
        public async Task<ApiResult<OrderResponse>> GetByIdAsync(string Id)
        {
            var order = await _unitOfWork.OrderRepository.FirstOrDefaultAsync(x => x.Id.ToString() == Id);
            var result = _mapper.Map<OrderResponse>(order);
            /// Returns the result of the order.
            if (result == null)
                return new ApiErrorResult<OrderResponse>("Not found the order");
            return new ApiSuccessResult<OrderResponse>(result);
        }
    }
}
