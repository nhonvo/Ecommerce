using Domain.Enums;
using Domain.Entities;

namespace Application.ViewModels.Order
{
    public class OrderHistoryResponse
    {
        public string Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShippingAddress { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public ICollection<OrderDetailsHistory> OrderDetails { get; set; }

    }
    public class OrderDetailsHistory
    {
        public BookHistory Book { get; set; }
        public int Quantity { get; set; }
    }
    public class BookHistory
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Inventory { get; set; }
        public string Genre { get; set; }
    }
}