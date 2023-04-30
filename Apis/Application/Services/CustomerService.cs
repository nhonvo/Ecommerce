using Application;
using Application.Commons;
using Application.Interfaces;
using Application.ViewModels.Customer;
using AutoMapper;
using Domain.Aggregate;
using Domain.Aggregate.AppResult;
using Domain.Entities;
using Domain.Interfaces;
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
                // _unitOfWork.BeginTransaction();
                // await _unitOfWork.CustomerRepository.AddAsync(customer);
                // await _unitOfWork.CommitAsync();
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
                _unitOfWork.BeginTransaction();
                _unitOfWork.CustomerRepository.Update(customer);
                await _unitOfWork.CommitAsync();
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
        public async Task<ApiResult<CustomerResponse>> GetByIdAsync(string Id)
        {
            var customer = await _unitOfWork.CustomerRepository.FirstOrDefaultAsync(x => x.Id.ToString() == Id);
            var result = _mapper.Map<CustomerResponse>(customer);
            /// Returns the result of the customer.
            if (result == null)
                return new ApiErrorResult<CustomerResponse>("Not found the customer");
            return new ApiSuccessResult<CustomerResponse>(result);
        }
    }
}
