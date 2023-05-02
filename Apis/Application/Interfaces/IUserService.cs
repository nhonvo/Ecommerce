using Application.ViewModels.UserViewModels;
using Domain.Aggregate.AppResult;

namespace Application.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Log into the server. This is an asynchronous operation. To determine whether the request has finished processing return a
        /// </summary>
        /// <param name="request">Contains the user's</param>
        Task<ApiResult<LoginResponse>> LoginAsync(LoginRequest request);
        /// <summary>
        /// Registers a new user. This will overwrite any existing registration for the user.
        /// </summary>
        /// <param name="user">Information about the new user to register with Nexpose</param>
        Task<ApiResult<bool>> RegisterAsync(RegisterRequest user);
        /// <summary>
        /// Purchases a user. This is a non - blocking call and will return immediately after completing the request.
        /// </summary>
        /// <param name="request">Information about the purchase request. It can include the user's email</param>
        Task<ApiResult<UserResponse>> Purchase(PurchaseRequest request);
        /// <summary>
        /// Gets the details of a user. Makes a GET request to the People resource.
        /// </summary>
        /// <param name="UserId">The ID of the user to get details for</param>
        Task<ApiResult<UserResponse>> Get(string UserId);
        Task<ApiResult<UserResponse>> Update(UserUpdateRequest request);
    }
}
