using Application.Interfaces;
using System.Security.Claims;

namespace WebAPI.Services
{
    public class ClaimsService : IClaimsService
    {
        private readonly IJWTService _jwtService;
        private string accessToken;

        public ClaimsService(IHttpContextAccessor httpContextAccessor, IJWTService jwtService)
        {
            _jwtService = jwtService;
            accessToken = httpContextAccessor.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()!;
            // var Id = _jwtService.Validate(accessToken).Claims.FirstOrDefault(c => c.Type == "ID")?.Value!;
            // CurrentUserId = string.IsNullOrEmpty(Id) ? Guid.Empty : Guid.Parse(Id);
        }


        public Guid CurrentUserId { get; }
    }
}
