using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;
using BartenderSupportSystem.Shared.Utils;

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
            CustomValidator.ValidateObject(item);
            if (item.Id == 0)
            {
                return new IngredientDbModel(item.ComponentId, item.CocktailId, item.ProportionType,
                    item.ProportionValue);
            }
            else
            {
                return new IngredientDbModel(item.Id, item.ComponentId, item.CocktailId, item.ProportionType,
                    item.ProportionValue);
            }

            ;
        }

        public IngredientDto ToDto(IngredientDbModel item)
        {
            CustomValidator.ValidateObject(item);
            var ingredientDtoToReturn = new IngredientDto
            {
                Id = item.Id,
                CocktailId = item.Id,
                ComponentId = item.ComponentId,
                ProportionType = item.ProportionType,
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
    }
}