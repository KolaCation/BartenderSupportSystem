using System;
using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;

namespace BartenderSupportSystem.Server.AppServices.RecommendationSystem.Drinks.Specifications
{
    internal sealed class BrandSpecification : ISpecification<DrinkDbModel>
    {
        public Guid BrandId { get; }

        public BrandSpecification(Guid brandId)
        {
            BrandId = brandId;
        }

        public bool IsSatisfied(DrinkDbModel item)
        {
            return item.BrandId.Equals(BrandId);
        }
    }
}
