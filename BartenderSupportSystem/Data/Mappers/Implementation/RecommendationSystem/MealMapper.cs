using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Data.DTO.RecommendationSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.RecommendationSystem
{
    internal sealed class MealMapper : IMealMapper
    {
        public MealDbModel ToDbModel(MealDto item)
        {
            if (item.Id == 0)
            {
                return new MealDbModel { Name = item.Name, PricePerGr = item.PricePerGr };
            }
            else
            {
                return new MealDbModel { Id = item.Id, Name = item.Name, PricePerGr = item.PricePerGr };
            }
        }

        public MealDto ToDto(MealDbModel item)
        {
            return new MealDto
            {
                Id = item.Id,
                Name = item.Name,
                PricePerGr = item.PricePerGr
            };
        }
    }
}