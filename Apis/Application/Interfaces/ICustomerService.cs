using Application.Commons;
using Application.ViewModels.Customer;
using Application.ViewModels.Order;
using Domain.Aggregate.AppResult;
namespace Application.Interfaces
{
    public interface ICustomerService
    {
        /// <summary>
        /// Retrieve a paged list of customers. You can use this method to retrieve a list of customers by passing page and page size parameters.
        /// </summary>
        /// <param name="pageIndex">The zero - based index of the page of results to return.</param>
        /// <param name="pageSize">The size of the page of results to return</param>
        Task<ApiResult<Pagination<CustomerResponse>>> GetAsync(int pageIndex, int pageSize);
        /// <summary>
        /// Gets the with the given identifier. Throws if the customer does not exist. Requires authentication. Api permissions required ( class - permission level ).
        /// </summary>
        /// <param name="Id">The identifier of the customer to retrieve. This can be found in</param>
        Task<ApiResult<CustomerResponse>> GetAsync(Guid Id);
        /// <summary>
        /// Adds a customer to the data source. Makes a POST request to the CaaS API and returns a CustomerResponse
        /// </summary>
        /// <param name="request">Information about the customer to be added to the data</param>
        Task<ApiResult<CustomerResponse>> AddAsync(CreateCustomer request);
        /// <summary>
        /// Updates an existing customer. Makes a PUT request to the CaaS API to update information about a customer
        /// </summary>
        /// <param name="request">Information needed to update a</param>
        Task<ApiResult<CustomerResponse>> Update(UpdateCustomer request);
        /// <summary>
        /// Deletes the customer with the specified identifier. Makes no changes to the customer's data. To add a customer to a group use the Add method.
        /// </summary>
        /// <param name="Id">Identifier of the customer to delete. Must be unique</param>
        Task<ApiResult<CustomerResponse>> Delete(string Id);
        /// <summary>
        /// Gets the order by id. Use this method to retrieve orders that belong to a customer. You can use pagination parameters to control which page of results are returned.
        /// </summary>
        /// <param name="id">The id of the customer for which you want to retrieve orders.</param>
        /// <param name="pageIndex">The page index of the query. This is zero - based.</param>
        /// <param name="pageSize">The size of the page. This is zero - based</param>
        Task<ApiResult<Pagination<CustomerResponse>>> GetOrder(Guid id, int pageIndex, int pageSize);
        /// <summary>
        /// Add an order to the system. This will be executed in the background and the result will be returned in the response
        /// </summary>
        /// <param name="id">Id of the order to</param>
        Task<ApiResult<OrderResponse>> AddOrder(Guid id);
        /// <summary>
        /// Update an existing order. This can be used to change the status of an order that is in progress or to add a new order to the customer
        /// </summary>
        /// <param name="id">Id of the customer to update</param>
        /// <param name="orderId">Id of the order to update must be unique</param>
        /// <param name="request">Information about the order to be updated must be</param>
        Task<ApiResult<OrderResponse>> UpdateOrder(Guid id, Guid orderId, UpdateCustomerOrder request);
        /// <summary>
        /// Delete an order from the store. This is a soft delete so don't use it for anything
        /// </summary>
        /// <param name="id">Id of the order to delete</param>
        /// <param name="orderId">Id of the order to delete from the</param>
        Task<ApiResult<OrderResponse>> DeleteOrder(Guid id, Guid orderId);
        /// <summary>
        /// Searches for customers based on search string. This is an asynchronous operation. The status of the operation will be tracked by the object that's returned.
        /// </summary>
        /// <param name="search">The string to search for. If this parameter is null or empty the search will be performed on all customers.</param>
        /// <param name="pageIndex">The page index of the customer to return.</param>
        /// <param name="pageSize">The size of the customer's page to return</param>
        Task<ApiResult<Pagination<CustomerResponse>>> Search(string search, int pageIndex, int pageSize);
        Task<ApiResult<Pagination<OrderResponse>>> GetCustomerOrderDetailsAsync(Guid customerId, int pageIndex = 0, int pageSize = 10);
    }
}
