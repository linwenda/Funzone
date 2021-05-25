using FluentValidation;

namespace Funzone.Application.Pages.Commands
{
    public class SavePageDraftCommandValidator : AbstractValidator<SavePageDraftCommand>
    {
        public SavePageDraftCommandValidator()
        {
            RuleFor(v => v.Title)
                .NotNull()
                .NotEmpty()
                .MaximumLength(64);
        }
    }
}