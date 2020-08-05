using System;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem
{
    internal sealed class IngredientDbModel
    {
        public Guid Id { get; private set; }
        public Guid ComponentId { get; private set; }
        public Guid CocktailId { get; private set; }
        public ProportionType ProportionType { get; private set; }
        public double ProportionValue { get; private set; }

        public IngredientDbModel(Guid id, Guid componentId, Guid cocktailId, ProportionType proportionType, double proportionValue)
        {
            Id = id;
            ComponentId = componentId;
            CocktailId = cocktailId;
            ProportionType = proportionType;
            ProportionValue = proportionValue;
        }
    }
}
