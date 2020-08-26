using System;
using System.Collections.Generic;
using System.Text;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Shared.Utils.RecommendationSystem.Drinks
{
    public sealed class DrinkFilterDto
    {
        public string Name { get; set; }
        public AlcoholType Type { get; set; }
        public double AlcoholPercentageMinValue { get; set; }
        public double AlcoholPercentageMaxValue { get; set; }
        public string Flavor { get; set; }
        public int BrandId { get; set; }
        public PriceSort PriceSort { get; set; }
    }
}
