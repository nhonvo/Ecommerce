using Application.ViewModels.Book;
using Domain.Entities;
using FluentValidation;

namespace WebAPI.Validations
{
    public class BookValidation : AbstractValidator<SearchRequest>
    {
        public BookValidation()
        {
            RuleFor(x => x.Title).MinimumLength(50);
        }

    }
}