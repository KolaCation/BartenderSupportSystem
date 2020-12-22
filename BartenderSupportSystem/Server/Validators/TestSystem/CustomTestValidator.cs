using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Shared.Models.TestSystem;
using FluentValidation;

namespace BartenderSupportSystem.Server.Validators.TestSystem
{
    public sealed class CustomTestValidator : AbstractValidator<CustomTestDto>
    {
        public CustomTestValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(2).WithMessage("Name must be at least 2 chars long.")
                .MaximumLength(60).WithMessage("Name must not exceed 60 chars.");
            RuleFor(e => e.Topic)
                .NotEmpty().WithMessage("Topic is required.")
                .MinimumLength(2).WithMessage("Topic must be at least 2 chars long.")
                .MaximumLength(60).WithMessage("Topic must not exceed 60 chars.");
            RuleFor(e => e.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(2).WithMessage("Description must be at least 2 chars long.")
                .MaximumLength(255).WithMessage("Description must not exceed 255 chars.");
            RuleFor(e => e.Questions)
                .NotEmpty().WithMessage("Questions are required.")
                .Must(list => list.Count >= 4).WithMessage("Test must have at least 4 questions.")
                .Must(list => list.Count <= 20).WithMessage("Test must not exceed 20 questions.");
            RuleForEach(e => e.Questions).SetValidator(new CustomQuestionValidator());
        }
    }
}
