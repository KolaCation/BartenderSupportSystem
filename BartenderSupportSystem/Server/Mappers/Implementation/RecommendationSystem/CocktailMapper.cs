using BartenderSupportSystem.Domain.RecommendationSystem;
using BartenderSupportSystem.Server.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Mappers.Interfaces.RecommendationSystem;

namespace BartenderSupportSystem.Server.Mappers.Implementation.RecommendationSystem
{
    internal sealed class CocktailMapper : ICocktailMapper
    {
        public Cocktail DbToDomain(CocktailDbModel dbModel)
        {
            return new Cocktail(dbModel.Id, dbModel.Name, dbModel.Type, dbModel.PhotoPath);
        }

        public CocktailDbModel DomainToDb(Cocktail domain)
        {
            return new CocktailDbModel(domain.Id, domain.Name, domain.Type, domain.PhotoPath);
        }
    }
}
