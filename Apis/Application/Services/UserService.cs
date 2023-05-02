using Application;
using Application.Interfaces;
using Application.ViewModels.UserViewModels;
using AutoMapper;
using Domain.Aggregate.AppResult;
using Domain.Entities;

namespace Infrastructures.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        public UserService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IJWTService jwtService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jwtService;
        }
        public async Task<ApiResult<UserResponse>> Get(string UserId)
        {

            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Id.ToString() == UserId);
            if (user == null)
                return new ApiErrorResult<UserResponse>("User not found");
            var result = _mapper.Map<UserResponse>(user);
            return new ApiSuccessResult<UserResponse>(result);
        }
        public async Task<ApiResult<UserResponse>> Update(UserUpdateRequest request)
        {

            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user == null)
                return new ApiErrorResult<UserResponse>("User not found");
            user.Address = request.Address;
            user.Password = _jwtService.Hash(request.Password);
            user.Name = request.Name; ;

            try
            {
                _unitOfWork.BeginTransaction();
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.CommitAsync();
                var result = _mapper.Map<UserResponse>(user);
                return new ApiSuccessResult<UserResponse>(result);
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<UserResponse>("Can't update book", new List<string> { ex.ToString() });
            }
        }
        public async Task<ApiResult<LoginResponse>> LoginAsync(LoginRequest request)
        {
            var user = await _unitOfWork.UserRepository.Find(request.Email);
            // verify passwordHash
            if (!_jwtService.Verify(request.Password, user.Password))
                return new ApiErrorResult<LoginResponse>("Incorrect Password!!!");
            var newUser = _mapper.Map<LoginResponse>(user);

            newUser.Token = _jwtService.GenerateJWT(user);

            newUser.ExpireDay = DateTime.Now.AddDays(1);

            return new ApiSuccessResult<LoginResponse>(newUser);
        }
        public async Task<ApiResult<bool>> RegisterAsync(RegisterRequest request)
        {
            var isExist = await _unitOfWork.UserRepository.CheckExistUser(request.Email);

            if (isExist)
                return new ApiErrorResult<bool>("Email already Exist!!!");

            var user = _mapper.Map<User>(request);
            user.Password = _jwtService.Hash(user.Password);

            try
            {
                _unitOfWork.BeginTransaction();
                await _unitOfWork.UserRepository.AddAsync(user);
                await _unitOfWork.CommitAsync();
                return new ApiSuccessResult<bool>();
            }
            catch (Exception exception)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<bool>("Register fail!! ", new List<string> { exception.ToString() });
            }
        }
        public async Task<ApiResult<UserResponse>> Purchase(PurchaseRequest request)
        {
            var user = await _unitOfWork.UserRepository.FirstOrDefaultAsync(x => x.Id.ToString() == request.UserId);
            if (user.CreditBalance < request.Amount)
                return new ApiErrorResult<UserResponse>("Can't purchase not enough credit");

            user.CreditBalance = user.CreditBalance - request.Amount;

            try
            {
                _unitOfWork.UserRepository.Update(user);
                await _unitOfWork.CommitAsync();
                var result = _mapper.Map<UserResponse>(user);
                return new ApiSuccessResult<UserResponse>(result);
            }
            catch (Exception exception)
            {
                _unitOfWork.Rollback();
                return new ApiErrorResult<UserResponse>("Something went wrong", new List<string> { exception.ToString() });
            }
        }
    }
}