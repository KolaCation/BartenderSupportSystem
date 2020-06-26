using System;
using System.Collections.Generic;
using System.Text;
using BartenderSupportSystem.Domain.RecommendationSystem.Enums;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Domain.RecommendationSystem
{
    public sealed class Cocktail
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CocktailType Type { get; set; }
        public string PhotoPath { get; set; }

        public void UpdatePhotoPath(string newPhotoPath)
        {
            PhotoPath = newPhotoPath;
        }
    }
}
