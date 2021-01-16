using System.Collections.Generic;

namespace BartenderSupportSystem.Server.Data.DTO.RecommendationSystem
{
    public sealed class CocktailDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CocktailType { get; set; }
        public string PhotoPath { get; set; }
        public List<IngredientDto> Ingredients { get; set; }
        public string Description { get; set; }
    }
}
