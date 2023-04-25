using Domain.Enums;

namespace Application.ViewModels.WishList
{
    public class UserWishlist
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public Role Role { get; set; } = Role.User;
    }
}