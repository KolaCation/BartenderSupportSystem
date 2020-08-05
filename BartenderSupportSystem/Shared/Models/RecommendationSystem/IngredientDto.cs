using System;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Shared.Models.RecommendationSystem
{
    public sealed class IngredientDto
    {
        public Guid Id { get; set; }
        public Guid ComponentId { get; set; }
        public MealDto Meal { get; set; }
        public DrinkDto Drink { get; set; }
        public Guid CocktailId { get; set; }
        public CocktailDto Cocktail { get; set; }
        public ProportionType ProportionType { get; set; }
        public double ProportionValue { get; set; }
    }
}