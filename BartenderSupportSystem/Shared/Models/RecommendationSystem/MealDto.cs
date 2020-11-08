using System;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Shared.Models.RecommendationSystem
{
    public sealed class MealDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double PricePerGr { get; set; }
    }
}
