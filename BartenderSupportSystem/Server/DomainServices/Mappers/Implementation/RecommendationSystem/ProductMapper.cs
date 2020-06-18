using BartenderSupportSystem.Domain.RecommendationSystem;
using BartenderSupportSystem.Server.DomainServices.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.DomainServices.Mappers.Interfaces.RecommendationSystem;

namespace BartenderSupportSystem.Server.DomainServices.Mappers.Implementation.RecommendationSystem
{
    internal sealed class ProductMapper : IProductMapper
    {
        public Product DbToDomain(ProductDbModel dbModel)
        {
            return new Product(dbModel.Id, dbModel.Name, dbModel.PricePerGr);
        }

        public ProductDbModel DomainToDb(Product domain)
        {
            return new ProductDbModel(domain.Id, domain.Name, domain.PricePerGr);
        }
    }
}
