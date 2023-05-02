using Application;
using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Customer;
using Application.ViewModels.Order;
using AutoMapper;
using Domain.Aggregate.AppResult;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructures.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomerService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        #region customer
        public async Task<ApiResult<Pagination<CustomerResponse>>> GetAsync(int pageIndex, int pageSize)
        {
            var result = await _unitOfWork.CustomerRepository.ToPagination(pageIndex, pageSize);
            var customers = _mapper.Map<Pagination<CustomerResponse>>(result);
            if (customers == null)
                return new ApiErrorResult<Pagination<CustomerResponse>>("Can't get customer");
            return new ApiSuccessResult<Pagination<CustomerResponse>>(customers);
        }


        public async Task<ApiResult<CustomerResponse>> AddAsync(CreateCustomer request)
        {
            var customer = _mapper.Map<Customer>(request);
            try
            {
                await _unitOfWork.ExecuteTransactionAsync(() => _unitOfWork.CustomerRepository.AddAsync(customer));
                var result = _mapper.Map<CustomerResponse>(customer);

                return new ApiSuccessResult<CustomerResponse>(result);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<CustomerResponse>("Can't add customer", new List<string> { ex.ToString() });
            }
        }
        public async Task<ApiResult<CustomerResponse>> Update(UpdateCustomer request)
        {
            var customerExist = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(x => x.Id == request.Id);
            /// Customer is not found.
            if (customerExist == null)
                return new ApiErrorResult<CustomerResponse>("Customer not found");
            var customer = _mapper.Map<Customer>(request);

            try
            {
                await _unitOfWork.ExecuteTransactionAsync(() => { _unitOfWork.CustomerRepository.Update(customer); });
                var result = _mapper.Map<CustomerResponse>(customer);
                return new ApiSuccessResult<CustomerResponse>(result);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<CustomerResponse>("Can't update customer", new List<string> { ex.ToString() });
            }
        }
        public async Task<ApiResult<CustomerResponse>> Delete(string Id)
        {
            var customer = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(x => x.Id.ToString() == Id);
            /// Return a CustomerResponse object if customer is null
            if (customer == null)
                return new ApiErrorResult<CustomerResponse>("Customer not found");
            try
            {
                await _unitOfWork.ExecuteTransactionAsync(() => _unitOfWork.CustomerRepository.Delete(customer));

                // _unitOfWork.BeginTransaction();
                // _unitOfWork.CustomerRepository.Delete(customer);
                // await _unitOfWork.CommitAsync();
                var result = _mapper.Map<CustomerResponse>(customer);
                return new ApiSuccessResult<CustomerResponse>(result);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<CustomerResponse>("Can't delete customer", new List<string> { ex.ToString() });
            }
        }
        public async Task<ApiResult<CustomerResponse>> GetAsync(Guid Id)
        {
            var customer = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(x => x.Id == Id);
            var result = _mapper.Map<CustomerResponse>(customer);
            /// Returns the result of the customer.
            if (result == null)
                return new ApiErrorResult<CustomerResponse>("Not found the customer");
            return new ApiSuccessResult<CustomerResponse>(result);
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
        #endregion
        #region  customer and order
        public async Task<ApiResult<Pagination<CustomerResponse>>> GetOrder(Guid Id, int pageIndex, int pageSize)
        {
            var order = await _unitOfWork.CustomerRepository.GetAsync(
                filter: x => x.Id == Id && x.Orders.Any(x => x.CustomerId == Id),
                include: x => x.Include(x => x.Orders),
                pageIndex: pageIndex,
                pageSize: pageSize);
            var result = _mapper.Map<Pagination<CustomerResponse>>(order);
            /// Returns the result: the order of customer.
            if (result == null)
                return new ApiErrorResult<Pagination<CustomerResponse>>("Not found the order");
            return new ApiSuccessResult<Pagination<CustomerResponse>>(result);
        }
        // TODO: OPTIMIZE enter list orderDetail
        public async Task<ApiResult<OrderResponse>> AddOrder(Guid id)
        {
            var order = new Order();
            order.CustomerId = id;
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
                await _unitOfWork.ExecuteTransactionAsync(() => { _unitOfWork.OrderRepository.AddAsync(order); });
                var result = _mapper.Map<OrderResponse>(order);
                return new ApiSuccessResult<OrderResponse>(result);
            }
            catch (Exception ex)
            {
                return new ApiErrorResult<OrderResponse>("Can't add the order", new List<string> { ex.ToString() });
            }
        }
        // FIXME: test
        public async Task<ApiResult<OrderResponse>> UpdateOrder(Guid id, Guid orderId, UpdateCustomerOrder request)
        {
            var order = await _unitOfWork.OrderRepository.FirstOrdDefaultAsync(
                filter: x => x.Id == orderId && x.Customer.Id == id
            );
            var updateOrder = _mapper.Map<Order>(request);
            order = updateOrder;
            try
            {
                await _unitOfWork.ExecuteTransactionAsync(() => { _unitOfWork.OrderRepository.Update(order); });
                var result = _mapper.Map<OrderResponse>(order);
                return new ApiSuccessResult<OrderResponse>(result);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<OrderResponse>("Can't update customer", new List<string> { ex.ToString() });
            }
            throw new NotImplementedException();
        }
        // FIXME: test
        public async Task<ApiResult<OrderResponse>> DeleteOrder(Guid id, Guid orderId)
        {
            var order = await _unitOfWork.OrderRepository.FirstOrdDefaultAsync(
                filter: x => x.Id == orderId && x.Customer.Id == id
            );
            try
            {
                await _unitOfWork.ExecuteTransactionAsync(() => { _unitOfWork.OrderRepository.Delete(order); });
                var result = _mapper.Map<OrderResponse>(order);
                return new ApiSuccessResult<OrderResponse>(result);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<OrderResponse>("Can't delete customer", new List<string> { ex.ToString() });
            }
        }
        #endregion
        public async Task<ApiResult<Pagination<CustomerResponse>>> Search(string search, int pageIndex, int pageSize)
        {
            var customers = await _unitOfWork.CustomerRepository.GetAsync(
                filter: x => x.Name.Contains(search)
                             || x.Email.Contains(search)
                             || x.Phone.Contains(search)
                             || x.Address.Contains(search),
                pageIndex: pageIndex,
                pageSize: pageSize
            );
            var result = _mapper.Map<Pagination<CustomerResponse>>(customers);
            if (result == null)
                return new ApiErrorResult<Pagination<CustomerResponse>>("Can't get customer");
            return new ApiSuccessResult<Pagination<CustomerResponse>>(result);
        }
        #region View customer orders with product details
        public async Task<ApiResult<Pagination<OrderDetails>>> GetCustomerOrderDetailsAsync(Guid customerId, int pageIndex = 0, int pageSize = 10)
        {
            var customer = await _unitOfWork.CustomerRepository.GetByIdAsync(customerId);
            if (customer == null)
                return new ApiErrorResult<Pagination<OrderDetails>>($"Customer with ID {customerId} not found.");

            var orders = await _unitOfWork.OrderRepository.GetAsync(
                filter: o => o.CustomerId == customerId,
                include: o => o.Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product),
                pageIndex: pageIndex,
                pageSize: pageSize);

            var orderDetails = orders.Items.SelectMany(o => o.OrderDetails).ToList();
            var orderDetailResponses = _mapper.Map<List<OrderDetails>>(orderDetails);

            return new ApiSuccessResult<Pagination<OrderDetails>>(new Pagination<OrderDetails>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = orders.TotalItemsCount,
                Items = orderDetailResponses,
            });
        }

        #endregion
    }
}
