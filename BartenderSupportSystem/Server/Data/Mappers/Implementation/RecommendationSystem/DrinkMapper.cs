using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;
using BartenderSupportSystem.Shared.Utils;
using Microsoft.AspNetCore.Server.IIS.Core;
using System;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.RecommendationSystem
{
    internal sealed class DrinkMapper : IDrinkMapper
    {
        public DrinkDbModel ToDbModel(DrinkDto item)
        {
            CustomValidator.ValidateObject(item);
            var alcoholType = Enum.TryParse(typeof(AlcoholType), item.AlcoholType, out var result);
            if (alcoholType)
            {
                if (item.Id == 0)
                {
                    return new DrinkDbModel(item.Name, (AlcoholType)result, item.AlcoholPercentage, item.Flavor, item.BrandId, item.PricePerMl, item.PhotoPath);
                }
                else
                {
                    return new DrinkDbModel(item.Id, item.Name, (AlcoholType)result, item.AlcoholPercentage, item.Flavor, item.BrandId, item.PricePerMl, item.PhotoPath);
                }
            }
            else
            {
                throw new InvalidCastException(nameof(result));
            }

        }

        public DrinkDto ToDto(DrinkDbModel item)
        {
            CustomValidator.ValidateObject(item);
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
