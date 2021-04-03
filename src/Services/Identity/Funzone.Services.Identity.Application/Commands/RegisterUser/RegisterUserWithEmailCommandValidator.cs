﻿using FluentValidation;

namespace Funzone.Services.Identity.Application.Commands.RegisterUser
{
    public class RegisterUserWithEmailCommandValidator : AbstractValidator<RegisterUserWithEmailCommand>
    {
        public RegisterUserWithEmailCommandValidator()
        {
            RuleFor(c => c.EmailAddress).EmailAddress();
            RuleFor(c => c.Password).NotNull().Length(6, 50);
        }
    }
}