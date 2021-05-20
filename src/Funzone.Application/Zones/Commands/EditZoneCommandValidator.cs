using FluentValidation;

namespace Funzone.Application.Zones.Commands
{
    public class EditZoneCommandValidator : AbstractValidator<EditZoneCommand>
    {
        public EditZoneCommandValidator()
        {
            RuleFor(v => v.Title).NotNull().NotEmpty().MaximumLength(20);
            RuleFor(v => v.Color).MaximumLength(20);
            RuleFor(v => v.Icon).MaximumLength(255);
        }
    }
}