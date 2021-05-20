﻿using FluentValidation;

namespace Funzone.Application.Zones.Commands
{
    public class CreateZoneCommandValidator : AbstractValidator<CreateZoneCommand>
    {
        public CreateZoneCommandValidator()
        {
            RuleFor(v => v.Title).NotNull().NotEmpty().MaximumLength(20);
            RuleFor(v => v.Color).MaximumLength(20);
            RuleFor(v => v.Icon).MaximumLength(255);
        }
    }
}