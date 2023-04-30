using Domain.Enums;

namespace Application.ViewModels.Order
{
    public class CreateOrder
    {
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}