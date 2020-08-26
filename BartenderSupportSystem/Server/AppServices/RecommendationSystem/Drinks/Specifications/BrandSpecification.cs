using System;
using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;

namespace BartenderSupportSystem.Server.AppServices.RecommendationSystem.Drinks.Specifications
{
    internal sealed class BrandSpecification : ISpecification<DrinkDbModel>
    {
        public int BrandId { get; }

        public BrandSpecification(int brandId)
        {
            BrandId = brandId;
        }

        public bool IsSatisfied(DrinkDbModel item)
        {
            return item.BrandId.Equals(BrandId);
        }
    }
}
