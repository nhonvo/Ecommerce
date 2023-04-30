using Application.ViewModels.Product;
using FluentValidation;

namespace WebAPI.Validations
{
    public class CreateProductValidation : AbstractValidator<CreateProduct>
    {
        public CreateProductValidation()
        {
            RuleFor(product => product.Name)
            .NotEmpty()
            .MaximumLength(50);

            RuleFor(product => product.Description)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(product => product.Price)
                .GreaterThan(0);
        }

    }
}