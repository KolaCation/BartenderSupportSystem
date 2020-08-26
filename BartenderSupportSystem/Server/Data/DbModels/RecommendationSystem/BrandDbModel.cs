using System;
using System.ComponentModel.DataAnnotations;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem
{
    internal sealed class BrandDbModel
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public Countries CountryOfOrigin { get; private set; }

        public BrandDbModel(string name, Countries countryOfOrigin)
        {
            CustomValidator.ValidateString(name, CustomValidatorDefaultValues.StrDefaultMinLength, CustomValidatorDefaultValues.StrDefaultMaxLength);
            Name = name;
            CountryOfOrigin = countryOfOrigin;
        }

        public BrandDbModel(int id, string name, Countries countryOfOrigin) : this(name, countryOfOrigin)
        {
            Id = id;
        }
    }
}
