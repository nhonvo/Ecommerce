using Domain.Enums;

namespace Application.ViewModels.UserViewModels
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public decimal CreditBalance { get; set; } = 0;
        public Role Role { get; set; }
    }
}
