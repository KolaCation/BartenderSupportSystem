using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Data.DTO.RecommendationSystem;
using BartenderSupportSystem.Server.Data.DTO.RecommendationSystem.Enums;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem;
using System;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.RecommendationSystem
{
    internal sealed class BrandMapper : IBrandMapper
    {
        public BrandDbModel ToDbModel(BrandDto item)
        {
            var brandCountry = Enum.TryParse(typeof(Countries), item.CountryOfOrigin, out var result);
            if (brandCountry)
            {
                if (item.Id == 0)
                {
                    return new BrandDbModel { Name = item.Name, CountryOfOrigin = (Countries)result };
                }
                else
                {
                    return new BrandDbModel { Id = item.Id, Name = item.Name, CountryOfOrigin = (Countries)result };
                }
            }
            else
            {
                throw new InvalidCastException(nameof(result));
            }
        }

        public BrandDto ToDto(BrandDbModel item)
        {
            return new BrandDto
            {
                Id = item.Id,
                Name = item.Name,
                CountryOfOrigin = item.CountryOfOrigin.ToString()
            };
        }
    }
}