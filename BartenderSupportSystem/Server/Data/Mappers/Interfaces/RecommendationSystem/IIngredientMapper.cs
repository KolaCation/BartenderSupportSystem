using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;

namespace BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem
{
    internal interface IIngredientMapper : IMapper<IngredientDto, IngredientDbModel>
    {
        List<IngredientDbModel> ToDbModelList(List<IngredientDto> items);
        List<IngredientDto> ToDtoList(List<IngredientDbModel> items);
    }
}