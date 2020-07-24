using System;
using System.ComponentModel.DataAnnotations;
using BartenderSupportSystem.Client.Helpers;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Client.ViewModels.RecommendationSystem
{
    public class CocktailViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [CheckForEnumValue(typeof(CocktailType))]
        public string Type { get; set; }
        public string PhotoPath { get; set; }

    }
}
