using System;

namespace BartenderSupportSystem.Server.DbModels.RecommendationSystem
{
    internal sealed class IngredientDbModel
    {
        public Guid Id { get; private set; }
        public Guid ComponentId { get; private set; }
        public Guid CocktailId { get; private set; }
        public double Weight { get; private set; }

        public IngredientDbModel(Guid id, Guid componentId, Guid cocktailId, double weight)
        {
            Id = id;
            ComponentId = componentId;
            CocktailId = cocktailId;
            Weight = weight;
        }
    }
}
