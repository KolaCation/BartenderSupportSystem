using BartenderSupportSystem.Domain.RecommendationSystem;
using BartenderSupportSystem.Server.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Mappers.Interfaces.RecommendationSystem;

namespace BartenderSupportSystem.Server.Mappers.Implementation.RecommendationSystem
{
    internal sealed class IngredientMapper : IIngredientMapper
    {
        public Ingredient DbToDomain(IngredientDbModel dbModel)
        {
            return new Ingredient(dbModel.Id, dbModel.ComponentId, dbModel.CocktailId, dbModel.Weight);
        }

        public IngredientDbModel DomainToDb(Ingredient domain)
        {
            return new IngredientDbModel(domain.Id, domain.ComponentId, domain.CocktailId, domain.Weight);
        }
    }
}
