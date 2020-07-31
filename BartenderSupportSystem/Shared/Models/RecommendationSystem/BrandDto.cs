using System;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Shared.Models.RecommendationSystem
{
    public sealed class BrandDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Countries CountryOfOrigin { get; set; }
    }
}
