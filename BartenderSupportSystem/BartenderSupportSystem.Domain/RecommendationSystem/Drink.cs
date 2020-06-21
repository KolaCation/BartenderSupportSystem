using System;
using System.Collections.Generic;
using System.Text;
using BartenderSupportSystem.Domain.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Domain.RecommendationSystem
{
    public sealed class Drink
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public AlcoholType Type { get; private set; }
        public double AlcoholPercentage { get; private set; }
        public string Flavor { get; private set; }
        public Guid BrandId { get; private set; }
        public double PricePerMl { get; private set; }
        public string PhotoPath { get; private set; }

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