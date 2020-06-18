using System;

namespace BartenderSupportSystem.Server.DomainServices.DbModels.RecommendationSystem
{
    internal sealed class MenuDbModel
    {
        public Guid Id { get; private set; }
        public Guid DrinkId { get; private set; }
        public Guid SnackId { get; private set; }

        public MenuDbModel(Guid id, Guid drinkId, Guid snackId)
        {
            Id = id;
            DrinkId = drinkId;
            SnackId = snackId;
        }
    }
}
