namespace Application.ViewModels.Order
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
    public class OrderDetails
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
    public class SalesReport
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalSales { get; set; }
        public List<DailySales> DailySales { get; set; }
    }

    public class DailySales
    {
        public DateTime Date { get; set; }
        public decimal TotalSales { get; set; }
    }
}