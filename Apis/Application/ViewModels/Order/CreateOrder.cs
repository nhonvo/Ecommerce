using Domain.Enums;

namespace Application.ViewModels.Order
{
    public class CreateOrder
    {
        public string UserId { get; set; }
        public string ShippingAddress { get; set; }
    }
}