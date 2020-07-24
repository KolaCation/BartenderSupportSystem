using System;

namespace BartenderSupportSystem.Shared.Models.RecommendationSystem
{
    public sealed class Ingredient
    {
        public Guid Id { get; set; }
        public Guid ComponentId { get; set; }
        public IComponent Component { get; set; }
        public Guid CocktailId { get; set; }
        public Cocktail Cocktail { get; set; }
        public double Weight { get; set; }
    }
}