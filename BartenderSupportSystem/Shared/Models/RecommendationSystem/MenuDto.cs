using System;

namespace BartenderSupportSystem.Shared.Models.RecommendationSystem
{
    public sealed class MenuDto
    {
        public Guid Id { get; set; }
        public Guid DrinkId { get; set; }
        public DrinkDto Drink { get; set; }
        public Guid MealId { get; set; }
        public MealDto Meal { get; set; }
    }
}
