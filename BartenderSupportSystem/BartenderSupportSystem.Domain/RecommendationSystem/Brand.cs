using System;
using BartenderSupportSystem.Domain.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Domain.RecommendationSystem
{
    public sealed class Brand
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Countries CountryOfOrigin { get; private set; }

        public Brand(Guid id, string name, Countries countryOfOrigin)
        {
            Id = id;
            Name = name;
            CountryOfOrigin = countryOfOrigin;
        }
    }
}
