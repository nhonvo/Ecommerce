using Domain.Entities;
using FluentValidation;

namespace WebAPI.Validations
{
    public class BookValidation : AbstractValidator<Book>
    {
        public BookValidation()
        {
            // RuleFor(x => x.message.ccRecipients)
            //     .Must(BeUnique)
            //     .WithMessage("The ccRecipients list must contain only unique values.");
        }

    }
}