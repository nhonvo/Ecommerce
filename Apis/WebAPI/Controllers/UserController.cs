using Application.Interfaces;

namespace WebAPI.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IClaimsService _claimService;

        public UserController(IUserService userService, IClaimsService claimsService)
        {
            _userService = userService;
            _claimService = claimsService;
        }
        // [HttpPost]
        // public async Task<ApiResult<bool>> Register(RegisterRequest request)
        //     => await _userService.RegisterAsync(request);
        // [HttpGet]
        // [Authorize]
        // public async Task<ApiResult<UserResponse>> Get()
        //     => await _userService.Get(_claimService.CurrentUserId.ToString());
        // [HttpPost]
        // public async Task<ApiResult<LoginResponse>> Login(LoginRequest request)
        // => await _userService.LoginAsync(request);
        // [HttpPost]
        // [Authorize]
        // public async Task<ApiResult<UserResponse>> Purchase(PurchaseRequest request)
        // => await _userService.Purchase(request);
        // [HttpPost]
        // [Authorize]
        // public async Task<ApiResult<UserResponse>> Update(UserUpdateRequest request)
        // => await _userService.Update(request);
    }
}