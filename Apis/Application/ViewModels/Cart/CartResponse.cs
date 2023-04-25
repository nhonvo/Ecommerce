using Application.ViewModels.Order;
using Application.ViewModels.WishList;

namespace Application.ViewModels.Book
{
    public class CartResponse
    {
        public UserWishlist User { get; set; }
        public BookHistory Book { get; set; }
        public int Quantity { get; set; }
    }

}