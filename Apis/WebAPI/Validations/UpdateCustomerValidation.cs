using Application.ViewModels.Customer;
using FluentValidation;
using System.Text.RegularExpressions;

namespace WebAPI.Validations
{
    public class UpdateCustomerValidation : AbstractValidator<UpdateCustomer>
    {
        public UpdateCustomerValidation()
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