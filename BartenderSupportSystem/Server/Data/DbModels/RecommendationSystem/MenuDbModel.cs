using System;

namespace BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem
{
    internal sealed class MenuDbModel
    {
        public Guid Id { get; private set; }
        public Guid DrinkId { get; private set; }
        public Guid MealId { get; private set; }

        public MenuDbModel(Guid id, Guid drinkId, Guid mealId)
        {
            Id = id;
            DrinkId = drinkId;
            MealId = mealId;
        }
    }
}
