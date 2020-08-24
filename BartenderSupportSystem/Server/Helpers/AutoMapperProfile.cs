using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BartenderSupportSystem.Server.Data.DbModels;
using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Shared.Models;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.TestSystem;

namespace BartenderSupportSystem.Server.Helpers
{
    public sealed class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BrandDto, BrandDbModel>().ReverseMap();
            CreateMap<CocktailDto, CocktailDbModel>().ReverseMap();
            CreateMap<DrinkDto, DrinkDbModel>().ReverseMap();
            CreateMap<IngredientDto, IngredientDbModel>().ReverseMap();
            CreateMap<MenuDto, MenuDbModel>().ReverseMap();
            CreateMap<BartenderDto, BartenderDbModel>().ReverseMap();
            CreateMap<CustomAnswerDto, CustomAnswerDbModel>().ReverseMap();
            CreateMap<CustomQuestionDto, CustomQuestionDbModel>().ReverseMap();
            CreateMap<CustomTestDto, CustomTestDbModel>().ReverseMap();
            CreateMap<MealDto, MealDbModel>().ReverseMap();
        }
    }
}
