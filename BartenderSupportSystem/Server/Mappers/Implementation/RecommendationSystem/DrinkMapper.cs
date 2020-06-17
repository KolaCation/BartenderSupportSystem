using BartenderSupportSystem.Domain.RecommendationSystem;
using BartenderSupportSystem.Server.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Mappers.Interfaces.RecommendationSystem;

namespace BartenderSupportSystem.Server.Mappers.Implementation.RecommendationSystem
{
    internal sealed class DrinkMapper : IDrinkMapper
    {
        public Drink DbToDomain(DrinkDbModel dbModel)
        {
            return new Drink(dbModel.Id, dbModel.Name, dbModel.Type, dbModel.AlcoholPercentage, dbModel.Flavor, dbModel.BrandId, dbModel.PricePerMl, dbModel.PhotoPath);
        }

        public DrinkDbModel DomainToDb(Drink domain)
        {
            return new DrinkDbModel(domain.Id, domain.Name, domain.Type, domain.AlcoholPercentage, domain.Flavor, domain.BrandId, domain.PricePerMl, domain.PhotoPath);
        }
    }
}
