using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using System.Collections.Generic;
using BartenderSupportSystem.Server.Data.DTO.RecommendationSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.Generic;

namespace BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem
{
    internal interface IIngredientMapper : IMapper<IngredientDto, IngredientDbModel>
    {
        List<IngredientDbModel> ToDbModelList(List<IngredientDto> items);
        List<IngredientDto> ToDtoList(List<IngredientDbModel> items);
    }
}