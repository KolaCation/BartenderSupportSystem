using System;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem
{
    internal sealed class IngredientDbModel
    {
        public int Id { get; private set; }
        public int ComponentId { get; private set; }
        public int CocktailId { get; private set; }
        public ProportionType ProportionType { get; private set; }
        public double ProportionValue { get; private set; }

        public IngredientDbModel(int componentId, int cocktailId, ProportionType proportionType, double proportionValue)
        {
            CustomValidator.ValidateNumber(proportionValue, CustomValidatorDefaultValues.NonNegativeDouble, 100000);
            ComponentId = componentId;
            CocktailId = cocktailId;
            ProportionType = proportionType;
            ProportionValue = proportionValue;
        }

        public IngredientDbModel(int id, int componentId, int cocktailId, ProportionType proportionType,
            double proportionValue) : this(componentId, cocktailId, proportionType, proportionValue)
        {
            Id = id;
        }
    }
}