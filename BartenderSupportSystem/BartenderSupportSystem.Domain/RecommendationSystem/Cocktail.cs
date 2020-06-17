using System;
using System.Collections.Generic;
using System.Text;
using BartenderSupportSystem.Domain.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Domain.RecommendationSystem
{
    public sealed class Cocktail
    {
        public Guid Id { get; }
        public string Name { get; }
        public CocktailType Type { get; }
        public string PhotoPath { get; }

        public Cocktail(Guid id, string name, CocktailType type, string photoPath)
        {
            Id = id;
            Name = name;
            Type = type;
            PhotoPath = photoPath;
        }
    }
}
