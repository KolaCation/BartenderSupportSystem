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
            RuleFor(e => e.Name).NotEmpty().WithMessage("Country of origin is required.");
            RuleFor(b => b.CountryOfOrigin).Must(BeValidCountryCode)
                .WithMessage("Provide a country from the list.");
        }

        private bool BeValidCountryCode(string countryCode)
        {
            var brandCountry = Enum.TryParse(typeof(Countries), countryCode, out var result);
            return brandCountry;
        }
    }
}
