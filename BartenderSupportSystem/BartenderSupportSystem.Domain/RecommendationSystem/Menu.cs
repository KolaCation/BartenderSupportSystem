using System;
using System.Collections.Generic;
using System.Text;

namespace BartenderSupportSystem.Domain.RecommendationSystem
{
    public sealed class Menu
    {
        public Guid Id { get; }
        public Guid DrinkId { get; }
        public Guid SnackId { get; }

        public Menu(Guid id, Guid drinkId, Guid snackId)
        {
            Id = id;
            DrinkId = drinkId;
            SnackId = snackId;
        }
    }
}
