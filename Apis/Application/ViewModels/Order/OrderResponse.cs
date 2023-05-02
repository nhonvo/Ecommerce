namespace Application.ViewModels.Order
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
    public class OrderDetails
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class SalesReport
    {
        public DateTime Date { get; set; }
        public decimal TotalSales { get; set; }
    }

}