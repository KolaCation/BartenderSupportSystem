using System;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem
{
    internal sealed class CocktailDbModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public CocktailType Type { get; private set; }
        public string PhotoPath { get; private set; }

        public CocktailDbModel(Guid id, string name, CocktailType type, string photoPath)
        {
            CustomValidator.ValidateId(id);
            CustomValidator.ValidateString(name, 2, 40);
            Id = id;
            Name = name;
            Type = type;
            PhotoPath = photoPath;
        }

        public void UpdatePhotoPath(string newPhotoPath)
        {
            PhotoPath = newPhotoPath;
        }
    }
}
