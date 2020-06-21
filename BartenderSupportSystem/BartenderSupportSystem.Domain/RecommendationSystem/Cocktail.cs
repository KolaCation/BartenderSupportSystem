using System;
using System.Collections.Generic;
using System.Text;
using BartenderSupportSystem.Domain.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Domain.RecommendationSystem
{
    public sealed class Cocktail
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public CocktailType Type { get; private set; }
        public string PhotoPath { get; private set; }

        public Cocktail(Guid id, string name, CocktailType type, string photoPath)
        {
            Id = id;
            Name = name;
            Type = type;
            PhotoPath = photoPath;
        }
    }
}
