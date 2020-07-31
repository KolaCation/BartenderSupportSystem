using System;

namespace BartenderSupportSystem.Shared.Models.RecommendationSystem
{
    public sealed class MenuDto
    {
        public Guid Id { get; set; }
        public Guid DrinkId { get; set; }
        public DrinkDto Drink { get; set; }
        public Guid SnackId { get; set; }
        public SnackDto Snack { get; set; }
    }
}
