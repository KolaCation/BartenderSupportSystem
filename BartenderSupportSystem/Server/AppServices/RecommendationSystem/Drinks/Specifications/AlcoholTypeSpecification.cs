using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Server.AppServices.RecommendationSystem.Drinks.Specifications
{
    internal sealed class AlcoholTypeSpecification : ISpecification<DrinkDbModel>
    {
        public AlcoholType Type { get; }

        public AlcoholTypeSpecification(AlcoholType type)
        {
            Type = type;
        }

        public bool IsSatisfied(DrinkDbModel item)
        {
            return item.Type.Equals(Type);
        }
    }
}
