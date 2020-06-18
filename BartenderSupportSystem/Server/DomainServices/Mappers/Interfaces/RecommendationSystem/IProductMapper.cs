using BartenderSupportSystem.Domain.RecommendationSystem;
using BartenderSupportSystem.Server.DomainServices.DbModels.RecommendationSystem;

namespace BartenderSupportSystem.Server.DomainServices.Mappers.Interfaces.RecommendationSystem
{
    internal interface IProductMapper : IMapper<Product, ProductDbModel>
    {
    }
}
