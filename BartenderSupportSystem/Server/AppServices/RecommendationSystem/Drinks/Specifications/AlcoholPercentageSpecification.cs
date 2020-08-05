using System;
using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;

namespace BartenderSupportSystem.Server.AppServices.RecommendationSystem.Drinks.Specifications
{
    internal sealed class AlcoholPercentageSpecification : ISpecification<DrinkDbModel>
    {
        public double AlcoholPercentage { get; }

        public AlcoholPercentageSpecification(double alcoholPercentage)
        {
            AlcoholPercentage = alcoholPercentage;
        }

        public bool IsSatisfied(DrinkDbModel item)
        {
            return Math.Abs(item.AlcoholPercentage - AlcoholPercentage) < 0.000001;
        }
    }
}
