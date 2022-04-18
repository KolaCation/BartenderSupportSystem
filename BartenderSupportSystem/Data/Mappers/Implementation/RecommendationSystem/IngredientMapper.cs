using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Data.DTO.RecommendationSystem;
using BartenderSupportSystem.Server.Data.DTO.RecommendationSystem.Enums;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.RecommendationSystem
{
    internal sealed class IngredientMapper : IIngredientMapper
    {
        private readonly ApplicationDbContext _context;
        private readonly IDrinkMapper _drinkMapper;
        private readonly IMealMapper _mealMapper;

        public IngredientMapper(ApplicationDbContext context)
        {
            _context = context;
            _drinkMapper = new DrinkMapper();
            _mealMapper = new MealMapper();
        }

        public IngredientDbModel ToDbModel(IngredientDto item)
        {
            var proportionType = Enum.TryParse(typeof(ProportionType), item.ProportionType, out var result);
            if (proportionType)
            {
                if (item.Id == 0)
                {
                    return new IngredientDbModel
                    {
                        ComponentId = item.ComponentId,
                        CocktailId = item.CocktailId,
                        ProportionType = (ProportionType)result,
                        ProportionValue = item.ProportionValue
                    };
                }
                else
                {
                    return new IngredientDbModel
                    {
                        Id = item.Id,
                        ComponentId = item.ComponentId,
                        CocktailId = item.CocktailId,
                        ProportionType = (ProportionType)result,
                        ProportionValue = item.ProportionValue
                    };
                }
            }
            else
            {
                throw new InvalidCastException(nameof(result));
            }
        }

        public IngredientDto ToDto(IngredientDbModel item)
        {
            var ingredientDtoToReturn = new IngredientDto
            {
                Id = item.Id,
                CocktailId = item.Id,
                ComponentId = item.ComponentId,
                ProportionType = item.ProportionType.ToString(),
                ProportionValue = item.ProportionValue
            };
            if (item.ProportionType.Equals(ProportionType.Milliliter))
            {
                var drinkDbModel = _context.DrinksSet.Find(item.ComponentId);
                var drinkDto = _drinkMapper.ToDto(drinkDbModel);
                ingredientDtoToReturn.Drink = drinkDto;
            }
            else
            {
                var mealDbModel = _context.MealsSet.Find(item.ComponentId);
                var mealDto = _mealMapper.ToDto(mealDbModel);
                ingredientDtoToReturn.Meal = mealDto;
            }

            return ingredientDtoToReturn;
        }

        public List<IngredientDbModel> ToDbModelList(List<IngredientDto> items)
        {
            return items.Select(ToDbModel).ToList();
        }

        public List<IngredientDto> ToDtoList(List<IngredientDbModel> items)
        {
            return items.Select(ToDto).ToList();
        }
    }
}