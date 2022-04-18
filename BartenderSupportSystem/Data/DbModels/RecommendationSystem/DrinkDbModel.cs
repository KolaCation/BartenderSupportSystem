using BartenderSupportSystem.Server.Data.DTO.RecommendationSystem.Enums;
using BartenderSupportSystem.Server.Helpers;
using System.ComponentModel.DataAnnotations;

namespace BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem
{
    internal sealed class DrinkDbModel
    {
        [Range(0, int.MaxValue)] public int Id { get; set; }

        [Required]
        [StringLength(DefaultConstants.StringMaxLength,
            MinimumLength = DefaultConstants.StringMinLength)]
        public string Name { get; set; }

        public AlcoholType Type { get; set; }
        [Range(0, 100)] public double AlcoholPercentage { get; set; }

        [Required]
        [StringLength(255,
            MinimumLength = DefaultConstants.StringMinLength)]
        public string Flavor { get; set; }

        [Range(0, int.MaxValue)] public int BrandId { get; set; }
        [Range(0, 100)] public double PricePerMl { get; set; }
        public string PhotoPath { get; set; }
    }
}