using System;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem
{
    internal sealed class CocktailDbModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public CocktailType Type { get; private set; }
        public string PhotoPath { get; private set; }
        public string Description { get; private set; }

        public CocktailDbModel(string name, CocktailType type, string description, string photoPath)
        {
            CustomValidator.ValidateString(name, CustomValidatorDefaultValues.StrDefaultMinLength, CustomValidatorDefaultValues.StrDefaultMaxLength);
            CustomValidator.ValidateString(description, CustomValidatorDefaultValues.StrDefaultMinLength, 255);
            Name = name;
            Type = type;
            PhotoPath = photoPath;
            Description = description;
        }

        public CocktailDbModel(int id, string name, CocktailType type, string description, string photoPath) : this(name, type, description, photoPath)
        {
            Id = id;
        }

        public void UpdatePhotoPath(string newPhotoPath)
        {
            PhotoPath = newPhotoPath;
        }
    }
}
