using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Shared.Models.TestSystem;
using FluentValidation;

namespace BartenderSupportSystem.Server.Validators.TestSystem
{
    public class CustomQuestionValidator : AbstractValidator<CustomQuestionDto>
    {
        public CustomQuestionValidator()
        {
            RuleFor(e => e.Statement)
                .NotEmpty().WithMessage("Statement is required.")
                .MinimumLength(2).WithMessage("Statement must be at least 2 chars long.")
                .MaximumLength(60).WithMessage("Statement must not exceed 60 chars.");
            RuleFor(e => e.Answers)
                .NotEmpty().WithMessage("Answers are required.")
                .Must(list => list.Count >= 2).WithMessage("Question must have at least 2 answers.")
                .Must(list => list.Count <= 6).WithMessage("Question must not exceed 6 answers.")
                .Must(list => list.Select(e => e.IsCorrect).ToList().Contains(true))
                .WithMessage("Question must have at least 1 correct answer.");
            RuleForEach(e => e.Answers).SetValidator(new CustomAnswerValidator());
        }
    }
}