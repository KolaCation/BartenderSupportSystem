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
            CreateMap<Cocktail, CocktailViewModel>().ReverseMap();
            CreateMap<Brand, BrandViewModel>().ForMember(dest => dest.CountryOfOrigin,
                opt => opt.MapFrom(source => source.CountryOfOrigin.ToString()));
            CreateMap<BrandViewModel, Brand>().ForMember(dest => dest.CountryOfOrigin,
                opt => opt.MapFrom(source => Enum.Parse(typeof(Countries), source.CountryOfOrigin)));
        }
    }
}
