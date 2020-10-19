using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.RecommendationSystem
{
    internal sealed class MealMapper : IMealMapper
    {
        public MealDbModel ToDbModel(MealDto item)
        {
            CustomValidator.ValidateObject(item);
            if (item.Id == 0)
            {
                return new MealDbModel(item.Name, item.PricePerGr, item.Type);
            }
            else
            {
                return new MealDbModel(item.Id, item.Name, item.PricePerGr, item.Type);
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
                Type = item.Type
            };
        }
    }
}