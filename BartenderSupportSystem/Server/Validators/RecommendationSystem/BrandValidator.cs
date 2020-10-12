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
            RuleFor(b => b.CountryOfOrigin).Must(BeValidCountryCode)
                .WithMessage("Country is not valid (server side validaton)");
        }

        private bool BeValidCountryCode(string countryCode)
        {
            var brandCountry = Enum.TryParse(typeof(Countries), countryCode, out var result);
            return brandCountry;
        }
    }
}
