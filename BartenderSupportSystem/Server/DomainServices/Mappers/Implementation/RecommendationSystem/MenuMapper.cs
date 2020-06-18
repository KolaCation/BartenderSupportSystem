using BartenderSupportSystem.Domain.RecommendationSystem;
using BartenderSupportSystem.Server.DomainServices.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.DomainServices.Mappers.Interfaces.RecommendationSystem;

namespace BartenderSupportSystem.Server.DomainServices.Mappers.Implementation.RecommendationSystem
{
    internal sealed class MenuMapper : IMenuMapper
    {
        public Menu DbToDomain(MenuDbModel dbModel)
        {
            return new Menu(dbModel.Id, dbModel.DrinkId, dbModel.SnackId);
        }

        public MenuDbModel DomainToDb(Menu domain)
        {
            return new MenuDbModel(domain.Id, domain.DrinkId, domain.SnackId);
        }
    }
}
