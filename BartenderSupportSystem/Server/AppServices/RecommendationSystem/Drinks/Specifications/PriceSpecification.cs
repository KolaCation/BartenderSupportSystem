using System;
using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;

namespace BartenderSupportSystem.Server.AppServices.RecommendationSystem.Drinks.Specifications
{
    internal sealed class PriceSpecification : ISpecification<DrinkDbModel>
    {
        public double Price { get; }

        public PriceSpecification(double price)
        {
            Price = price;
        }

        public bool IsSatisfied(DrinkDbModel item)
        {
            return Math.Abs(item.PricePerMl - Price) < 0.000001;
        }
    }
}
