using System;

namespace BartenderSupportSystem.Shared.Models.RecommendationSystem
{
    public sealed class MenuDto
    {
        public int Id { get; set; }
        public int DrinkId { get; set; }
        public DrinkDto Drink { get; set; }
        public int MealId { get; set; }
        public MealDto Meal { get; set; }
    }
}
