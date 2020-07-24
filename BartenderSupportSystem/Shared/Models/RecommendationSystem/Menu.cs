using System;

namespace BartenderSupportSystem.Shared.Models.RecommendationSystem
{
    public sealed class Menu
    {
        public Guid Id { get; set; }
        public Guid DrinkId { get; set; }
        public Drink Drink { get; set; }
        public Guid SnackId { get; set; }
        public Snack Snack { get; set; }
    }
}
