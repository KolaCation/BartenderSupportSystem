using System;
using System.Collections.Generic;
using System.Text;
using BartenderSupportSystem.Domain.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Domain.RecommendationSystem
{
    public sealed class Ingredient
    {
        public Guid Id { get; }
        public Guid ComponentId { get; }
        public Guid CocktailId { get; }
        public double Weight { get; }

        public Ingredient(Guid id, Guid componentId, Guid cocktailId, double weight)
        {
            Id = id;
            ComponentId = componentId;
            CocktailId = cocktailId;
            Weight = weight;
        }
    }
}