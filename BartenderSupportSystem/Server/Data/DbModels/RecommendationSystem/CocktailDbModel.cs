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

        public CocktailDbModel(string name, CocktailType type, string photoPath)
        {
            CustomValidator.ValidateString(name, CustomValidatorDefaultValues.StrDefaultMinLength, CustomValidatorDefaultValues.StrDefaultMaxLength);
            Name = name;
            Type = type;
            PhotoPath = photoPath;
        }

        public CocktailDbModel(int id, string name, CocktailType type, string photoPath) : this(name, type, photoPath)
        {
            Id = id;
        }

        public void UpdatePhotoPath(string newPhotoPath)
        {
            PhotoPath = newPhotoPath;
        }
    }
}
