using System;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Server.DomainServices.DbModels.RecommendationSystem
{
    internal sealed class DrinkDbModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public AlcoholType Type { get; private set; }
        public double AlcoholPercentage { get; private set; }
        public string Flavor { get; private set; }
        public Guid BrandId { get; private set; }
        public double PricePerMl { get; private set; }
        public string PhotoPath { get; private set; }

        public DrinkDbModel(Guid id, string name, AlcoholType type, double alcoholPercentage, string flavor, Guid brandId, double pricePerMl, string photoPath)
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
