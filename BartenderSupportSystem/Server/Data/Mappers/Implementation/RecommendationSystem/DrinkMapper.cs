using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.RecommendationSystem
{
    internal sealed class DrinkMapper : IDrinkMapper
    {
        public DrinkDbModel ToDbModel(DrinkDto item)
        {
            CustomValidator.ValidateObject(item);
            if (item.Id == 0)
            {
                return new DrinkDbModel(item.Name, item.Type, item.AlcoholPercentage, item.Flavor, item.BrandId, item.PricePerMl, item.PhotoPath);
            }
            else
            {
                return new DrinkDbModel(item.Id, item.Name, item.Type, item.AlcoholPercentage, item.Flavor, item.BrandId, item.PricePerMl, item.PhotoPath);
            }
        }

        public DrinkDto ToDto(DrinkDbModel item)
        {
            CustomValidator.ValidateObject(item);
            return new DrinkDto
            {
                Id = item.Id,
                Name = item.Name,
                Type = item.Type,
                AlcoholPercentage = item.AlcoholPercentage,
                Flavor = item.Flavor,
                BrandId = item.BrandId,
                PricePerMl = item.PricePerMl,
                PhotoPath = item.PhotoPath
            };
        }
    }
}
