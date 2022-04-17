using BartenderSupportSystem.Server.Data.DTO.RecommendationSystem.Enums;
using BartenderSupportSystem.Server.Helpers;
using System.ComponentModel.DataAnnotations;

namespace BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem
{
    internal sealed class CocktailDbModel
    {
        [Range(0, int.MaxValue)] public int Id { get; set; }

        [Required]
        [StringLength(DefaultConstants.StringMaxLength,
            MinimumLength = DefaultConstants.StringMinLength)]
        public string Name { get; set; }

        public CocktailType Type { get; set; }
        public string PhotoPath { get; set; }

        [Required]
        [StringLength(255,
            MinimumLength = DefaultConstants.StringMinLength)]
        public string Description { get; set; }
    }
}