using BartenderSupportSystem.Domain.RecommendationSystem;
using BartenderSupportSystem.Server.DbModels.RecommendationSystem;

namespace BartenderSupportSystem.Server.Mappers.Interfaces.RecommendationSystem
{
    internal interface IIngredientMapper : IMapper<Ingredient, IngredientDbModel>
    {
    }
}
