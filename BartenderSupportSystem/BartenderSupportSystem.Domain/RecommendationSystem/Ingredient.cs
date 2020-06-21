using System;
using System.Collections.Generic;
using System.Text;
using BartenderSupportSystem.Domain.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Domain.RecommendationSystem
{
    public sealed class Ingredient
    {
        public Guid Id { get; private set; }
        public Guid ComponentId { get; private set; }
        public Guid CocktailId { get; private set; }
        public double Weight { get; private set; }

        public Ingredient(Guid id, Guid componentId, Guid cocktailId, double weight)
        {
            Id = id;
            ComponentId = componentId;
            CocktailId = cocktailId;
            Weight = weight;
        }
    }
}