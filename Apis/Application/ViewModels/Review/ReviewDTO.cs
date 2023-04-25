
namespace Application.ViewModels.Review
{
    public class ReviewDTO
    {
        public Guid BookId { get; set; }
        public Guid UserId { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }
    }
}