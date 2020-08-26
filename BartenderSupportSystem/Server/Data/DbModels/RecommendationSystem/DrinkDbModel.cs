using System;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem
{
    internal sealed class DrinkDbModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public AlcoholType Type { get; private set; }
        public double AlcoholPercentage { get; private set; }
        public string Flavor { get; private set; }
        public int BrandId { get; private set; }
        public double PricePerMl { get; private set; }
        public string PhotoPath { get; private set; }

        public DrinkDbModel(string name, AlcoholType type, double alcoholPercentage, string flavor, int brandId, double pricePerMl, string photoPath)
        {
            CustomValidator.ValidateString(name, CustomValidatorDefaultValues.StrDefaultMinLength, CustomValidatorDefaultValues.StrDefaultMaxLength);
            CustomValidator.ValidateNumber(alcoholPercentage, CustomValidatorDefaultValues.NonNegativeDouble, 100);
            CustomValidator.ValidateString(flavor, 2, 255);
            CustomValidator.ValidateNumber(pricePerMl, CustomValidatorDefaultValues.NonNegativeDouble, 100);
            Name = name;
            Type = type;
            AlcoholPercentage = alcoholPercentage;
            Flavor = flavor;
            BrandId = brandId;
            PricePerMl = pricePerMl;
            PhotoPath = photoPath;
        }

        public DrinkDbModel(int id, string name, AlcoholType type, double alcoholPercentage, string flavor, int brandId, double pricePerMl, string photoPath) : this(name, type, alcoholPercentage, flavor, brandId, pricePerMl, photoPath)
        {
            Id = id;
        }



        public void UpdatePhotoPath(string newPhotoPath)
        {
            PhotoPath = newPhotoPath;
        }
    }
}
