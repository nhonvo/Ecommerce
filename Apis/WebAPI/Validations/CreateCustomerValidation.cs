using System.Text.RegularExpressions;
using Application.ViewModels.Customer;
using FluentValidation;

namespace WebAPI.Validations
{
    public class CreateCustomerValidation : AbstractValidator<CreateCustomer>
    {
        public CreateCustomerValidation()
        {
            RuleFor(customer => customer.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(customer => customer.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Invalid email format");

            RuleFor(customer => customer.Phone)
                .NotEmpty()
                .Matches(new Regex(@"^\d{10,}$"))
                .WithMessage("Invalid phone format");

            RuleFor(customer => customer.Address)
                .NotEmpty()
                .MaximumLength(100);
        }

    }
}