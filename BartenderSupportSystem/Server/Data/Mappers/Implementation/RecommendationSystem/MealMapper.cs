using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.RecommendationSystem
{
    internal sealed class MealMapper : IMealMapper
    {
        public MealDbModel ToDbModel(MealDto item)
        {
            CustomValidator.ValidateObject(item);
            var mealType = Enum.TryParse(typeof(MealType), item.MealType, out var result);
            if (mealType)
            {
                if (item.Id == 0)
                {
                    return new MealDbModel(item.Name, item.PricePerGr, (MealType) result);
                }
                else
                {
                    return new MealDbModel(item.Id, item.Name, item.PricePerGr, (MealType) result);
                }
            }
            else
            {
                throw new InvalidCastException(nameof(result));
            }
        }

        public MealDto ToDto(MealDbModel item)
        {
            CustomValidator.ValidateObject(item);
            return new MealDto
            {
                Id = item.Id,
                Name = item.Name,
                PricePerGr = item.PricePerGr,
                MealType = item.Type.ToString()
            };
        }
    }
}