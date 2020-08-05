using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem
{
    internal sealed class MealDbModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double PricePerGr { get; set; }
        public MealType Type { get; set; }

        public MealDbModel(Guid id, string name, double pricePerGr, MealType type)
        {
            Id = id;
            Name = name;
            PricePerGr = pricePerGr;
            Type = type;
        }
    }
}
