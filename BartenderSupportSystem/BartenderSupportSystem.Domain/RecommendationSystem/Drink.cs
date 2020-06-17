using System;
using System.Collections.Generic;
using System.Text;
using BartenderSupportSystem.Domain.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Domain.RecommendationSystem
{
    public sealed class Drink
    {
        public Guid Id { get; }
        public string Name { get; }
        public AlcoholType Type { get; }
        public double AlcoholPercentage { get; }
        public string Flavor { get; }
        public Guid BrandId { get; }
        public double PricePerMl { get; }
        public string PhotoPath { get; }

        public Drink(Guid id, string name, AlcoholType type, double alcoholPercentage, string flavor, Guid brandId, double pricePerMl, string photoPath)
        {
            Id = id;
            Name = name;
            Type = type;
            AlcoholPercentage = alcoholPercentage;
            Flavor = flavor;
            BrandId = brandId;
            PricePerMl = pricePerMl;
            PhotoPath = photoPath;
        }
    }
}