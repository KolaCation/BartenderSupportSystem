using System;
using BartenderSupportSystem.Domain.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Domain.RecommendationSystem
{
    public sealed class Brand
    {
        public Guid Id { get; }
        public string Name { get; }
        public Countries CountryOfOrigin { get; }

        public Brand(Guid id, string name, Countries countryOfOrigin)
        {
            Id = id;
            Name = name;
            CountryOfOrigin = countryOfOrigin;
        }
    }
}
