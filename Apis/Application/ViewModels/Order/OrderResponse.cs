using Domain.Enums;

namespace Application.ViewModels.Order
{
    public class OrderResponse
    {
        public string Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShippingAddress { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}