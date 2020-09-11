using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;
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
            var brandCountry = Enum.TryParse(typeof(Countries), item.CountryOfOrigin, out var result);
            if(brandCountry)
            {
                if (item.Id == 0)
                {
                    return new BrandDbModel(item.Name, (Countries)result);
                }
                else
                {
                    return new BrandDbModel(item.Id, item.Name, (Countries)result);
                }
            }
            else
            {
                throw new InvalidCastException(nameof(result));
            }

        }

        public BrandDto ToDto(BrandDbModel item)
        {
            CustomValidator.ValidateObject(item);
            return new BrandDto
            {
                Id = item.Id,
                Name = item.Name,
                CountryOfOrigin = item.CountryOfOrigin.ToString()
            };
        }
    }
}
