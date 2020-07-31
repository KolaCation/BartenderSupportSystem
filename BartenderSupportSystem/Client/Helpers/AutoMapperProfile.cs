using AutoMapper;
using BartenderSupportSystem.Client.ViewModels.RecommendationSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Client.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CocktailDto, CocktailViewModel>().ForMember(dest => dest.Type, opt => opt.MapFrom(source => source.Type.ToString()));
            CreateMap<CocktailViewModel, CocktailDto>().ForMember(dest => dest.Type, opt => opt.MapFrom(source => Enum.Parse(typeof(CocktailType), source.Type)));
            CreateMap<BrandDto, BrandViewModel>().ForMember(dest => dest.CountryOfOrigin, opt => opt.MapFrom(source => source.CountryOfOrigin.ToString()));
            CreateMap<BrandViewModel, BrandDto>().ForMember(dest => dest.CountryOfOrigin, opt => opt.MapFrom(source => Enum.Parse(typeof(Countries), source.CountryOfOrigin)));
            CreateMap<DrinkDto, DrinkViewModel>().ForMember(dest => dest.Type, opt => opt.MapFrom(source => source.Type.ToString()));
            CreateMap<DrinkViewModel, DrinkDto>().ForMember(dest => dest.Type, opt => opt.MapFrom(source => Enum.Parse(typeof(AlcoholType), source.Type)));
        }
    }
}