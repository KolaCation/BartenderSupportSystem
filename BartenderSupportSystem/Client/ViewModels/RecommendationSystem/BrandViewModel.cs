using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Client.Helpers;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Client.ViewModels.RecommendationSystem
{
    public sealed class BrandViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 2)]
        public string Name { get; set; }
        [Required]
        [CheckForEnumValue(typeof(Countries))]
        public string CountryOfOrigin { get; set; }
    }
}
