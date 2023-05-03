using Application;
using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Order;
using Application.ViewModels.OrderDetails;
using AutoMapper;
using Domain.Aggregate.AppResult;
using Domain.Entities;
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
        #region order
        public async Task<ApiResult<Pagination<OrderResponse>>> GetAsync(int pageIndex, int pageSize)
        {
            var result = await _unitOfWork.OrderRepository.ToPagination(pageIndex, pageSize);
            var orders = _mapper.Map<Pagination<OrderResponse>>(result);
            if (orders == null)
                return new ApiErrorResult<Pagination<OrderResponse>>("Can't get order");
            return new ApiSuccessResult<Pagination<OrderResponse>>(orders);
        }
        private async Task<decimal> CalculateTotalPriceAsync(IEnumerable<OrderDetail> orderDetails)
        {
            decimal totalPrice = 0;

            foreach (var item in orderDetails)
            {
                var product = await _unitOfWork.ProductRepository.FirstOrDefaultAsync(x => x.Id == item.ProductId);
                /// if product is not found for this ProductId
                if (product == null)
                {
                    throw new Exception($"Product not found for ProductId: {item.ProductId}");
                }

                decimal itemPrice = product.Price * item.Quantity;
                totalPrice += itemPrice;
            }

            return totalPrice;
        }
        public async Task<ApiResult<OrderResponse>> AddAsync(CreateOrder request)
        {
            var order = _mapper.Map<Order>(request);

            order.OrderDate = DateTime.Now;
            order.OrderDetails = new List<OrderDetail>{
                new OrderDetail{
                    ProductId = new Guid("00000001-0000-0000-0000-000000000000"),
                    Quantity = 3
                },
                new OrderDetail{
                    ProductId = new Guid("00000002-0000-0000-0000-000000000000"),
                    Quantity = 1
                },
            };
            order.TotalAmount = await CalculateTotalPriceAsync(order.OrderDetails);
            try
            {
                await _unitOfWork.ExecuteTransactionAsync(() => _unitOfWork.OrderRepository.AddAsync(order));

                var result = _mapper.Map<OrderResponse>(order);

                return new ApiSuccessResult<OrderResponse>(result);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<OrderResponse>(
                    "Can't add order",
                    new List<string> { ex.ToString() });
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
                await _unitOfWork.ExecuteTransactionAsync(() =>
                {
                    _unitOfWork.OrderRepository.Update(order);
                });
                var result = _mapper.Map<OrderResponse>(order);
                return new ApiSuccessResult<OrderResponse>(result);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<OrderResponse>(
                    "Can't update order",
                    new List<string> { ex.ToString() });
            }
        }
        public async Task<ApiResult<OrderResponse>> Delete(Guid Id)
        {
            var order = await _unitOfWork.OrderRepository.FirstOrDefaultAsync(x => x.Id == Id);
            /// Return a OrderResponse object if order is null
            if (order == null)
                return new ApiErrorResult<OrderResponse>("Order not found");
            try
            {
                await _unitOfWork.ExecuteTransactionAsync(() => _unitOfWork.OrderRepository.Delete(order));


                var result = _mapper.Map<OrderResponse>(order);
                return new ApiSuccessResult<OrderResponse>(result);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<OrderResponse>(
                    "Can't delete order",
                    new List<string> { ex.ToString() });
            }
        }
        public async Task<ApiResult<OrderResponse>> GetAsync(Guid Id)
        {
            var order = await _unitOfWork.OrderRepository.FirstOrDefaultAsync(x => x.Id == Id);
            var result = _mapper.Map<OrderResponse>(order);
            /// Returns the result of the order.
            if (result == null)
                return new ApiErrorResult<OrderResponse>("Not found the order");
            return new ApiSuccessResult<OrderResponse>(result);
        }
        #endregion
        #region Order OrderDetail
        public async Task<ApiResult<Pagination<OrderResponse>>> GetOrder(Guid Id, int pageIndex, int pageSize)
        {
            var order = await _unitOfWork.OrderRepository.GetAsync(
                filter: x => x.Id == Id,
                include: x => x.Include(x => x.OrderDetails),
                pageIndex: pageIndex,
                pageSize: pageSize);
            var result = _mapper.Map<Pagination<OrderResponse>>(order);
            /// Returns the result: the order of order.
            if (result == null)
                return new ApiErrorResult<Pagination<OrderResponse>>("Not found the order");
            return new ApiSuccessResult<Pagination<OrderResponse>>(result);
        }
        public async Task<ApiResult<OrderResponse>> AddOrder(Guid Id, AddOrderDetail request)
        {
            var orderDetail = _mapper.Map<OrderDetail>(request);

            var order = await _unitOfWork.OrderRepository.FirstOrdDefaultAsync(
                filter: x => x.Id == Id,
                include: x => x.Include(x => x.OrderDetails)
            );
            if (order == null)
                return new ApiErrorResult<OrderResponse>("Not found the order");

            var existOrderDetail = await _unitOfWork.OrderDetailRepository.FirstOrdDefaultAsync(x => x.ProductId == request.ProductId);
            _unitOfWork.BeginTransaction();
            if (existOrderDetail != null)
            {
                existOrderDetail.Quantity += request.Quantity;
                _unitOfWork.OrderDetailRepository.Update(existOrderDetail);
                order.TotalAmount = await CalculateTotalPriceAsync(await _unitOfWork.OrderDetailRepository.GetAsync(x => x.Id != null));
            }
            else
            {
                order.OrderDate = DateTime.Now;
                await _unitOfWork.OrderDetailRepository.AddAsync(orderDetail);
                order.TotalAmount = await CalculateTotalPriceAsync(order.OrderDetails);
            }
            try
            {
                await _unitOfWork.CommitAsync();

                var result = _mapper.Map<OrderResponse>(order);

                return new ApiSuccessResult<OrderResponse>(result);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<OrderResponse>("Can't add order", new List<string> { ex.ToString() });
            }
        }
        public async Task<ApiResult<OrderResponse>> UpdateOrder(Guid Id, UpdateOrderDetail request)
        {
            var orderDetail = _mapper.Map<OrderDetail>(request);

            var order = await _unitOfWork.OrderRepository.FirstOrdDefaultAsync(
                filter: x => x.Id == Id,
                include: x => x.Include(x => x.OrderDetails));

            var updateOrderDetail = order.OrderDetails.FirstOrDefault(x => x.OrderId == order.Id && x.ProductId == request.ProductId);

            if (updateOrderDetail == null)
                return new ApiErrorResult<OrderResponse>("Can not found order details");
            updateOrderDetail.Quantity = orderDetail.Quantity;

            order.OrderDate = DateTime.Now;
            order.TotalAmount = await CalculateTotalPriceAsync(order.OrderDetails);

            try
            {
                await _unitOfWork.ExecuteTransactionAsync(() => _unitOfWork.OrderRepository.Update(order));

                var result = _mapper.Map<OrderResponse>(order);

                return new ApiSuccessResult<OrderResponse>(result);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<OrderResponse>("Can't update order", new List<string> { ex.ToString() });
            }
        }
        public async Task<ApiResult<OrderDetails>> DeleteOrder(Guid orderId, Guid productId)
        {
            var orderDetail = await _unitOfWork.OrderDetailRepository.FirstOrdDefaultAsync(x => x.OrderId == orderId && x.ProductId == productId);
            if (orderDetail == null)
                return new ApiErrorResult<OrderDetails>("Order detail not found");

            try
            {
                await _unitOfWork.ExecuteTransactionAsync(() => _unitOfWork.OrderDetailRepository.Delete(orderDetail));
                var result = _mapper.Map<OrderDetails>(orderDetail);

                return new ApiSuccessResult<OrderDetails>(result);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<OrderDetails>("Can't delete order", new List<string> { ex.ToString() });
            }
        }
        #endregion
        public async Task<ApiResult<Pagination<OrderResponse>>> Search(
            string search,
            int pageIndex,
            int pageSize)
        {
            var orders = await _unitOfWork.OrderRepository.GetAsync(
                filter: x => x.OrderDate.ToString().Contains(search)
                             || x.TotalAmount.ToString().Contains(search),
                pageIndex: pageIndex,
                pageSize: pageSize
            );
            var result = _mapper.Map<Pagination<OrderResponse>>(orders);
            if (result == null)
                return new ApiErrorResult<Pagination<OrderResponse>>("Can't get order");
            return new ApiSuccessResult<Pagination<OrderResponse>>(result);
        }

    }
}
