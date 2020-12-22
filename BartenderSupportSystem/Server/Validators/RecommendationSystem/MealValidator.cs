using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;
using FluentValidation;

namespace BartenderSupportSystem.Server.Validators.RecommendationSystem
{
    public sealed class MealValidator : AbstractValidator<MealDto>
    {
        public MealValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(2).WithMessage("Name must be at least 2 chars long.")
                .MaximumLength(60).WithMessage("Name must not exceed 60 chars.");
            RuleFor(e => e.PricePerGr)
                .GreaterThanOrEqualTo(0).WithMessage("Min value: 0.")
                .LessThanOrEqualTo(100).WithMessage("Max value: 10000.");
        }
    }
}
