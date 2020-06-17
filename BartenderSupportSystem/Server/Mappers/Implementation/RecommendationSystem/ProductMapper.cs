using BartenderSupportSystem.Domain.RecommendationSystem;
using BartenderSupportSystem.Server.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Mappers.Interfaces.RecommendationSystem;

namespace BartenderSupportSystem.Server.Mappers.Implementation.RecommendationSystem
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
