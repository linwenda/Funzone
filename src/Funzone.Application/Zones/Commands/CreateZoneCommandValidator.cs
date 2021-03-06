﻿using FluentValidation;

namespace Funzone.Application.Zones.Commands
{
    public class CreateZoneCommandValidator : AbstractValidator<CreateZoneCommand>
    {
        public CreateZoneCommandValidator()
        {
            RuleFor(v => v.Title).NotNull().NotEmpty().MaximumLength(50);
            RuleFor(v => v.Description).MaximumLength(255);
        }
    }
}