namespace Application.ViewModels.Product
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
    public class TopSellingProduct
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int TotalQuantity { get; set; }
    }
}