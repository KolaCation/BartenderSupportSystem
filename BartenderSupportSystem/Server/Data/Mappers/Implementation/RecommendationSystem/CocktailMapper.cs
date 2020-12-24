using System;
using System.Linq;
using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.RecommendationSystem
{
    internal sealed class CocktailMapper : ICocktailMapper
    {
        private readonly IIngredientMapper _ingredientMapper;
        private readonly ApplicationDbContext _context;

        public CocktailMapper(ApplicationDbContext context)
        {
            _ingredientMapper = new IngredientMapper(context);
            _context = context;
        }

        public CocktailDbModel ToDbModel(CocktailDto item)
        {
            CustomValidator.ValidateObject(item);
            var cocktailType = Enum.TryParse(typeof(CocktailType), item.CocktailType, out var result);
            if (cocktailType)
            {
                if (item.Id == 0)
                {
                    return new CocktailDbModel(item.Name, (CocktailType) result, item.Description, item.PhotoPath);
                }
                else
                {
                    return new CocktailDbModel(item.Id, item.Name, (CocktailType) result, item.Description,
                        item.PhotoPath);
                }
            }
            else
            {
                throw new InvalidCastException(nameof(result));
            }
        }

        public CocktailDto ToDto(CocktailDbModel item)
        {
            CustomValidator.ValidateObject(item);
            var ingredientDbModels = _context.IngredientsSet.Where(e => e.CocktailId.Equals(item.Id)).ToList();
            var ingredients = ingredientDbModels.Count == 0 ? null : _ingredientMapper.ToDtoList(ingredientDbModels);
            return new CocktailDto
            {
                Id = item.Id,
                CocktailType = item.Type.ToString(),
                Description = item.Description,
                Name = item.Name,
                PhotoPath = item.PhotoPath,
                Ingredients = ingredients
            };
        }
    }
}