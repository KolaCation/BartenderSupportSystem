using FluentValidation;
using System;
using BartenderSupportSystem.Server.Data.DTO.RecommendationSystem;
using BartenderSupportSystem.Server.Data.DTO.RecommendationSystem.Enums;

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
