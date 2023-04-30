using Application.Interfaces;

namespace WebAPI.Controllers
{
    public class OrderDetailController : BaseController
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }
    }
}