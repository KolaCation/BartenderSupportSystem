using System;

namespace BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem
{
    internal sealed class MenuDbModel
    {
        public int Id { get; private set; }
        public int DrinkId { get; private set; }
        public int MealId { get; private set; }

        public MenuDbModel(int id, int drinkId, int mealId)
        {
            Id = id;
            DrinkId = drinkId;
            MealId = mealId;
        }
    }
}
