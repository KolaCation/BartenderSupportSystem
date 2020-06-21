using System;
using System.Collections.Generic;
using System.Text;

namespace BartenderSupportSystem.Domain.RecommendationSystem
{
    public sealed class Menu
    {
        public Guid Id { get; private set; }
        public Guid DrinkId { get; private set; }
        public Guid SnackId { get; private set; }

        public Menu(Guid id, Guid drinkId, Guid snackId)
        {
            Id = id;
            DrinkId = drinkId;
            SnackId = snackId;
        }
    }
}
