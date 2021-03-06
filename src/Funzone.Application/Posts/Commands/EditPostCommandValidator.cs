﻿using FluentValidation;

namespace Funzone.Application.Posts.Commands
{
    public class EditPostCommandValidator : AbstractValidator<EditPostCommand>
    {
        public EditPostCommandValidator()
        {
            RuleFor(v => v.Title)
                .NotNull().NotEmpty().MaximumLength(50);

            RuleFor(v => v.Content)
                .MaximumLength(2048);
        }
    }
}