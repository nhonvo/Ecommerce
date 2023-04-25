namespace Application.ViewModels.Order
{
    public class UpdateOrder
    {

        public Guid Id { get; set; }
        public string UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShippingAddress { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}