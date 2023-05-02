using Application;
using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Order;
using Application.ViewModels.OrderDetails;
using Application.ViewModels.Product;
using AutoMapper;
using Domain.Aggregate.AppResult;
using Domain.Entities;
using Domain.Enums;
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
        // FIXME: test
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
        // FIXME: test
        public async Task<ApiResult<OrderResponse>> AddOrder(Guid Id, AddOrderDetail request)
        {
            var orderDetail = _mapper.Map<OrderDetail>(request);

            var order = new Order();

            order.OrderDate = DateTime.Now;
            order.OrderDetails = (ICollection<OrderDetail>)orderDetail;
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
                return new ApiErrorResult<OrderResponse>("Can't add order", new List<string> { ex.ToString() });
            }
        }
        // FIXME: test
        public async Task<ApiResult<OrderResponse>> UpdateOrder(Guid Id, UpdateOrderDetail request)
        {
            var orderDetail = _mapper.Map<OrderDetail>(request);

            var order = await _unitOfWork.OrderRepository.FirstOrdDefaultAsync(x => x.Id == Id);
            order.OrderDate = DateTime.Now;
            order.OrderDetails = (ICollection<OrderDetail>)orderDetail;
            order.TotalAmount = await CalculateTotalPriceAsync(order.OrderDetails);
            try
            {
                await _unitOfWork.ExecuteTransactionAsync(() => _unitOfWork.OrderRepository.Update(order));

                var result = _mapper.Map<OrderResponse>(order);

                return new ApiSuccessResult<OrderResponse>(result);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<OrderResponse>("Can't update order", new List<string> { ex.ToString() });
            }
        }
        // FIXME: test
        public async Task<ApiResult<OrderResponse>> DeleteOrder(Guid Id)
        {
            var order = await _unitOfWork.OrderRepository.FirstOrdDefaultAsync(x => x.Id == Id);
            try
            {
                await _unitOfWork.ExecuteTransactionAsync(() => _unitOfWork.OrderRepository.Delete(order));

                var result = _mapper.Map<OrderResponse>(order);

                return new ApiSuccessResult<OrderResponse>(result);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<OrderResponse>("Can't delete order", new List<string> { ex.ToString() });
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
        #region Statistics
        // Total number of orders placed by a customer
        // Top-selling products in a given time period
        // Average order value
        // Revenue generated by a customer
        public async Task<ApiResult<int>> GetCustomerOrdersCountAsync(Guid customerId)
        {
            var ordersCount = await _unitOfWork.OrderRepository.CountAsync(
                filter: o => o.CustomerId == customerId);
            return new ApiSuccessResult<int>(ordersCount);
        }

        public async Task<ApiResult<Pagination<TopSellingProduct>>> GetTopSellingProducts(
            DateTime start,
            DateTime end,
            int pageIndex = 0,
            int pageSize = 10)
        {
            var orderDetails = await _unitOfWork.OrderDetailRepository.GetAsync(
                filter: od => od.Order.OrderDate >= start && od.Order.OrderDate <= end,
                include: od => od.Include(od => od.Product),
                pageIndex: pageIndex,
                pageSize: pageSize,
                sortColumn: od => od.Quantity,
                sortDirection: SortDirection.Descending);

            var products = orderDetails.Items.GroupBy(od => od.Product)
                .Select(group => new
                {
                    Product = group.Key,
                    TotalQuantity = group.Sum(od => od.Quantity)
                })
                .OrderByDescending(group => group.TotalQuantity)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .Select(group => new TopSellingProduct
                {
                    ProductId = group.Product.Id,
                    ProductName = group.Product.Name,
                    TotalQuantity = group.TotalQuantity
                })
                .ToList();

            if (products == null)
                return new ApiErrorResult<Pagination<TopSellingProduct>>("Can't get product");

            var pagination = new Pagination<TopSellingProduct>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = orderDetails.TotalItemsCount,
                Items = products
            };
            return new ApiSuccessResult<Pagination<TopSellingProduct>>(pagination);
        }
        public async Task<ApiResult<decimal>> GetAverageOrderValue(DateTime start, DateTime end)
        {
            var orderDetails = await _unitOfWork.OrderDetailRepository.GetAsync(
                filter: od => od.Order.OrderDate >= start && od.Order.OrderDate <= end,
                include: od => od.Include(od => od.Product),
                pageIndex: 0,
                pageSize: int.MaxValue);


            decimal totalPrice = 0;
            foreach (var item in orderDetails.Items)
            {
                var product = await _unitOfWork.ProductRepository.FirstOrDefaultAsync(x => x.Id == item.ProductId);
                if (product == null)
                {
                    throw new Exception($"Product not found for ProductId: {item.ProductId}");
                }

                decimal itemPrice = product.Price * item.Quantity;
                totalPrice += itemPrice;
            }

            int totalOrders = orderDetails.TotalItemsCount;
            decimal averageOrderValue = totalOrders > 0 ? totalPrice / totalOrders : 0;

            return new ApiSuccessResult<decimal>(averageOrderValue);
        }
        public async Task<ApiResult<decimal>> GetCustomerRevenue(Guid customerId)
        {
            // Retrieve all orders placed by the customer
            var orders = await _unitOfWork.OrderRepository.GetAsync(
                filter: o => o.CustomerId == customerId,
                include: o => o.Include(o => o.OrderDetails),
                pageIndex: 0,
                pageSize: int.MaxValue);

            // Calculate the total revenue from the orders
            decimal totalRevenue = orders.Items.Sum(o =>
            {
                decimal orderTotal = 0;
                foreach (var item in o.OrderDetails)
                {
                    var product = _unitOfWork.ProductRepository.GetByIdAsync(item.ProductId);
                    if (product == null)
                    {
                        throw new Exception($"Product not found for ProductId: {item.ProductId}");
                    }
                    orderTotal += item.Product.Price * item.Quantity;
                }
                return orderTotal;
            });
            return new ApiSuccessResult<decimal>(totalRevenue);
        }
        #endregion
    }
}
