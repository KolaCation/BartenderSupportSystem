using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;

namespace BartenderSupportSystem.Server.AppServices.RecommendationSystem.Drinks.Specifications
{
    internal sealed class NameSpecification : ISpecification<DrinkDbModel>

    {
        public string Name { get; }

        public NameSpecification(string name)
        {
            Name = name;
        }

        public bool IsSatisfied(DrinkDbModel item)
        {
            return item.Name.ToLower().Contains(Name.ToLower());
        }
    }
}