using BartenderSupportSystem.Domain.RecommendationSystem;
using BartenderSupportSystem.Server.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Mappers.Interfaces.RecommendationSystem;

namespace BartenderSupportSystem.Server.Mappers.Implementation.RecommendationSystem
{
    internal sealed class BrandMapper : IBrandMapper
    {
        public Brand DbToDomain(BrandDbModel dbModel)
        {
            return new Brand(dbModel.Id, dbModel.Name, dbModel.CountryOfOrigin);
        }

        public BrandDbModel DomainToDb(Brand domain)
        {
            return new BrandDbModel(domain.Id, domain.Name, domain.CountryOfOrigin);
        }
    }
}
