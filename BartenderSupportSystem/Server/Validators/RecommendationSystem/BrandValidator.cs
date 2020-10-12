using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Server.Validators.RecommendationSystem
{
    public sealed class BrandValidator : AbstractValidator<BrandDto>
    {
        public BrandValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(2).WithMessage("Name must be at least 2 chars long.")
                .MaximumLength(60).WithMessage("Name must not exceed 60 chars.");
            RuleFor(e => e.CountryOfOrigin)
                .NotEmpty().WithMessage("Country of origin is required.")
                .Must(BeValidCountryCode).WithMessage("Provide a country from the list.");
        }

        private bool BeValidCountryCode(string countryCode)
        {
            var brandCountry = Enum.TryParse(typeof(Countries), countryCode, out var result);
            return brandCountry;
        }
    }
}
