namespace Application.ViewModels.OrderDetails
{
    public class OrderDetailsDTO
    {
        public Guid BookId { get; set; }
        public Guid OrderId { get; set; }
        public int Quantity { get; set; }
    }
}