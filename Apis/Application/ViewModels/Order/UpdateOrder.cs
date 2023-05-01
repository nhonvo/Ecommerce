namespace Application.ViewModels.Order
{
    public class UpdateOrder
    {

        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public class UpdateCustomerOrder
    {

        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}