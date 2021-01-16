using System;
using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Data.DTO.RecommendationSystem;
using BartenderSupportSystem.Server.Data.DTO.RecommendationSystem.Enums;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem;


namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.RecommendationSystem
{
    internal sealed class DrinkMapper : IDrinkMapper
    {
        public DrinkDbModel ToDbModel(DrinkDto item)
        {
            var alcoholType = Enum.TryParse(typeof(AlcoholType), item.AlcoholType, out var result);
            if (alcoholType)
            {
                if (item.Id == 0)
                {
                    return new DrinkDbModel
                    {
                        Name = item.Name, Type = (AlcoholType) result, AlcoholPercentage = item.AlcoholPercentage,
                        Flavor = item.Flavor, BrandId = item.BrandId, PricePerMl = item.PricePerMl,
                        PhotoPath = item.PhotoPath
                    };
                }
                else
                {
                    return new DrinkDbModel
                    {
                        Id = item.Id, Name = item.Name, Type = (AlcoholType) result,
                        AlcoholPercentage = item.AlcoholPercentage, Flavor = item.Flavor, BrandId = item.BrandId,
                        PricePerMl = item.PricePerMl, PhotoPath = item.PhotoPath
                    };
                }
            }
            else
            {
                throw new InvalidCastException(nameof(result));
            }
        }

        public DrinkDto ToDto(DrinkDbModel item)
        {
            return new DrinkDto
            {
                Id = item.Id,
                Name = item.Name,
                AlcoholType = item.Type.ToString(),
                AlcoholPercentage = item.AlcoholPercentage,
                Flavor = item.Flavor,
                BrandId = item.BrandId,
                PricePerMl = item.PricePerMl,
                PhotoPath = item.PhotoPath
            };
        }
    }
}