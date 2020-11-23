using BartenderSupportSystem.Shared.Models.TestSystem;
using FluentValidation;

namespace BartenderSupportSystem.Server.Validators.TestSystem
{
    public sealed class CustomAnswerValidator : AbstractValidator<CustomAnswerDto>
    {
        public CustomAnswerValidator()
        {
            RuleFor(e => e.Statement)
                .NotEmpty().WithMessage("Statement is required.")
                .MinimumLength(2).WithMessage("Statement must be at least 2 chars long.")
                .MaximumLength(60).WithMessage("Statement must not exceed 60 chars.");
        }
    }
}
