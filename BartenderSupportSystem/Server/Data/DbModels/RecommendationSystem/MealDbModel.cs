using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem
{
    internal sealed class MealDbModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public double PricePerGr { get; private set; }
        public MealType Type { get; private set; }

        public MealDbModel(string name, double pricePerGr, MealType type)
        {
            CustomValidator.ValidateString(name, CustomValidatorDefaultValues.StrDefaultMinLength,
                CustomValidatorDefaultValues.StrDefaultMaxLength);
            CustomValidator.ValidateNumber(pricePerGr, CustomValidatorDefaultValues.NonNegativeDouble, 100);
            Name = name;
            PricePerGr = pricePerGr;
            Type = type;
        }

        public MealDbModel(int id, string name, double pricePerGr, MealType type) : this(name, pricePerGr, type)
        {
            Id = id;
        }
    }
}