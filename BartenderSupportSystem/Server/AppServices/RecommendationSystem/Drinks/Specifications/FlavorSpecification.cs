using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;

namespace BartenderSupportSystem.Server.AppServices.RecommendationSystem.Drinks.Specifications
{
    internal sealed class FlavorSpecification : ISpecification<DrinkDbModel>
    {
        public string Flavor { get; }

        public FlavorSpecification(string flavor)
        {
            Flavor = flavor;
        }

        public bool IsSatisfied(DrinkDbModel item)
        {
            return item.Flavor.Contains(Flavor);
        }
    }
}
