using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Client.Helpers;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Client.ViewModels.RecommendationSystem
{
    public class DrinkViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [CheckForEnumValue(typeof(AlcoholType))]
        public string Type { get; set; }
        [Required]
        [Range(0, 99)]
        public double AlcoholPercentage { get; set; }
        [Required]
        public string Flavor { get; set; }
        [Required]
        [NotGuidEmpty]
        public Guid BrandId { get; set; }
        public BrandDto Brand { get; set; }
        [Required]
        [Range(0, 10000)]
        public double PricePerMl { get; set; }
        public string PhotoPath { get; set; }
    }
}
