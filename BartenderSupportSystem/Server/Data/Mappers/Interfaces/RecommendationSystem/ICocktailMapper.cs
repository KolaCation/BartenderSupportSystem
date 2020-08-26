using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem
{
    internal interface ICocktailMapper : IMapper<CocktailDto, CocktailDbModel>
    {
    }
}
