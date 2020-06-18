using BartenderSupportSystem.Domain.RecommendationSystem;
using BartenderSupportSystem.Server.DomainServices.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.DomainServices.Mappers.Interfaces.RecommendationSystem;

namespace BartenderSupportSystem.Server.DomainServices.Mappers.Implementation.RecommendationSystem
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
