using FluentValidation;

namespace Funzone.Application.Pages.Commands
{
    public class CreatePageCommandValidator : AbstractValidator<CreatePageCommand>
    {
        public CreatePageCommandValidator()
        {
            RuleFor(v => v.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(64);
        }
    }
}