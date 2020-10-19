using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.RecommendationSystem
{
    internal sealed class CocktailMapper : ICocktailMapper
    {
        public CocktailDbModel ToDbModel(CocktailDto item)
        {
            throw new NotImplementedException();
        }

        public CocktailDto ToDto(CocktailDbModel item)
        {
            throw new NotImplementedException();
        }
    }
}
