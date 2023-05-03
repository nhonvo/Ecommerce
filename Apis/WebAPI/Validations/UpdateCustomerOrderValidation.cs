using Application.ViewModels.Order;
using FluentValidation;

namespace WebAPI.Validations
{
    public class UpdateCustomerOrderValidation : AbstractValidator<UpdateCustomerOrder>
    {
        public UpdateCustomerOrderValidation()
        {
            RuleFor(order => order.OrderDate)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Now);

            RuleFor(order => order.TotalAmount)
                .GreaterThan(0);
        }

    }
}