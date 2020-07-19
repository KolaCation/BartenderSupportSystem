using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Client.ViewModels.RecommendationSystem
{
    public class DrinkViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AlcoholType Type { get; set; }
        public double AlcoholPercentage { get; set; }
        public string Flavor { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
        public double PricePerMl { get; set; }
        public string PhotoPath { get; set; }
    }
}
