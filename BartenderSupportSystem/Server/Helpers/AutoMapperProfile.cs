using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BartenderSupportSystem.Domain;
using BartenderSupportSystem.Domain.RecommendationSystem;
using BartenderSupportSystem.Domain.TestSystem;
using BartenderSupportSystem.Server.DomainServices.DbModels;
using BartenderSupportSystem.Server.DomainServices.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.DomainServices.DbModels.TestSystem;

namespace BartenderSupportSystem.Server.Helpers
{
    public sealed class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Brand, BrandDbModel>().ReverseMap();
            CreateMap<Cocktail, CocktailDbModel>().ReverseMap();
            CreateMap<Drink, DrinkDbModel>().ReverseMap();
            CreateMap<Ingredient, IngredientDbModel>().ReverseMap();
            CreateMap<Menu, MenuDbModel>().ReverseMap();
            CreateMap<Product, ProductDbModel>().ReverseMap();
            CreateMap<Snack, SnackDbModel>().ReverseMap();
            CreateMap<Bartender, BartenderDbModel>().ReverseMap();
            CreateMap<CustomAnswer, CustomAnswerDbModel>().ReverseMap();
            CreateMap<CustomQuestion, CustomQuestionDbModel>().ReverseMap();
            CreateMap<CustomTest, CustomTestDbModel>().ReverseMap();
        }
    }
}
