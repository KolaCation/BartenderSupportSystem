using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;

namespace BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem
{
    internal interface IMealMapper : IMapper<MealDto, MealDbModel>
    {
    }
}
