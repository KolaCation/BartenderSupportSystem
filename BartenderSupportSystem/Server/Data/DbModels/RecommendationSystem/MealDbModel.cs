using System.ComponentModel.DataAnnotations;
using BartenderSupportSystem.Server.Helpers;

namespace BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem
{
    internal sealed class MealDbModel
    {
        [Range(0, int.MaxValue)] public int Id { get; set; }

        [Required]
        [StringLength(DefaultConstants.StringMaxLength,
            MinimumLength = DefaultConstants.StringMinLength)]
        public string Name { get; set; }

        [Range(0, 100)] public double PricePerGr { get; set; }
    }
}