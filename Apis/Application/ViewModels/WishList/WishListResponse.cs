using Application.ViewModels.Order;

namespace Application.ViewModels.WishList
{
    public class WishListResponse
    {
        public UserWishlist User { get; set; }
        public BookHistory Book { get; set; }
    }
}