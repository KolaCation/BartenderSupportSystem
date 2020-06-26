using AutoMapper;
using BartenderSupportSystem.Client.ViewModels.RecommendationSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;

namespace BartenderSupportSystem.Client.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Cocktail, CocktailViewModel>().ReverseMap();
        }
    }
}
