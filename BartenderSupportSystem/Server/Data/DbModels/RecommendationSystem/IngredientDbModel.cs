using System.ComponentModel.DataAnnotations;
using BartenderSupportSystem.Server.Data.DTO.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem
{
    internal sealed class IngredientDbModel
    {
        [Range(0, int.MaxValue)] public int Id { get; set; }
        [Range(0, int.MaxValue)] public int ComponentId { get; set; }
        [Range(0, int.MaxValue)] public int CocktailId { get; set; }
        public ProportionType ProportionType { get; set; }
        [Range(0, 100000)] public double ProportionValue { get; set; }
    }
}