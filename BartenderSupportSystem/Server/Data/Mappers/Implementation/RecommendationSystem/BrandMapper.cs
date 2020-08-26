using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using BartenderSupportSystem.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.RecommendationSystem
{
    internal sealed class BrandMapper : IBrandMapper
    {
        public BrandDbModel ToDbModel(BrandDto item)
        {
            CustomValidator.ValidateObject(item);
            if (item.Id == 0)
            {
                return new BrandDbModel(item.Name, item.CountryOfOrigin);
            }
            else
            {
                return new BrandDbModel(item.Id, item.Name, item.CountryOfOrigin);
            }
        }

        public BrandDto ToDto(BrandDbModel item)
        {
            CustomValidator.ValidateObject(item);
            return new BrandDto
            {
                Id = item.Id,
                Name = item.Name,
                CountryOfOrigin = item.CountryOfOrigin
            };
        }
    }
}
