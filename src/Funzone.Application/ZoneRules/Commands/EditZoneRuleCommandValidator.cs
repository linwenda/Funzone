﻿using FluentValidation;

namespace Funzone.Application.ZoneRules.Commands
{
    public class EditZoneRuleCommandValidator : AbstractValidator<EditZoneRuleCommand>
    {
        public EditZoneRuleCommandValidator()
        {
            RuleFor(v => v.Title).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(v => v.Description).MaximumLength(128);
        }
    }
}