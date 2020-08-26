using System;
using System.Collections.Generic;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Shared.Models.RecommendationSystem
{
    public sealed class CocktailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CocktailType Type { get; set; }
        public string PhotoPath { get; set; }
        public List<IngredientDto> Ingredients { get; set; }
    }
}
