using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Server.Validators.RecommendationSystem
{
    public sealed class DrinkValidator : AbstractValidator<DrinkDto>
    {
        public DrinkValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(2).WithMessage("Name must be at least 2 chars long.")
                .MaximumLength(60).WithMessage("Name must not exceed 60 chars.");
            RuleFor(e => e.AlcoholType)
                .NotEmpty().WithMessage("Alcohol type is required.")
                .Must(BeValidAlcoholType).WithMessage("Provide a type from the list.");
            RuleFor(e => e.AlcoholPercentage)
                .GreaterThanOrEqualTo(0).WithMessage("Min value: 0.")
                .LessThanOrEqualTo(100).WithMessage("Max value: 100.");
            RuleFor(e => e.Flavor)
                .NotEmpty().WithMessage("Flavor is required.")
                .MinimumLength(2).WithMessage("Flavor must be at least 2 chars long.")
                .MaximumLength(255).WithMessage("Flavor must not exceed 255 chars.");
            RuleFor(e => e.PricePerMl)
                .GreaterThanOrEqualTo(0).WithMessage("Min value: 0.")
                .LessThanOrEqualTo(100).WithMessage("Max value: 10000.");
            RuleFor(e => e.BrandId)
                .NotEqual(0).WithMessage("Select a brand from the list.");
        }

        private bool BeValidAlcoholType(string alcoholType)
        {
            var alcoholTypeEnum = Enum.TryParse(typeof(AlcoholType), alcoholType, out var result);
            return alcoholTypeEnum;
        }
    }
}
