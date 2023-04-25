
using Application.ViewModels.Order;
using Application.ViewModels.WishList;

namespace Application.ViewModels.Review
{
    public class ReviewResponse
    {
        public Guid Id { get; set; }
        public UserWishlist User { get; set; }
        public BookHistory Book { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
    }
}