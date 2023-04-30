using Application.ViewModels.Order;
using FluentValidation;

namespace WebAPI.Validations
{
    public class CreateOrderValidation : AbstractValidator<CreateOrder>
    {
        public CreateOrderValidation()
        {
            RuleFor(order => order.CustomerId)
            .NotEmpty()
            .NotEqual(Guid.Empty);

            RuleFor(order => order.OrderDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now);

            RuleFor(order => order.TotalAmount)
                .GreaterThan(0);
        }

    }
}