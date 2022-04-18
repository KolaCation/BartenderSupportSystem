using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Data.DTO.RecommendationSystem;
using BartenderSupportSystem.Server.Data.DTO.RecommendationSystem.Enums;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem;
using System;
using System.Linq;

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
            var cocktailType = Enum.TryParse(typeof(CocktailType), item.CocktailType, out var result);
            if (cocktailType)
            {
                if (item.Id == 0)
                {
                    return new CocktailDbModel
                    {
                        Name = item.Name,
                        Type = (CocktailType)result,
                        Description = item.Description,
                        PhotoPath = item.PhotoPath
                    };
                }
                else
                {
                    return new CocktailDbModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Type = (CocktailType)result,
                        Description = item.Description,
                        PhotoPath = item.PhotoPath
                    };
                }
            }
            else
            {
                throw new InvalidCastException(nameof(result));
            }
        }

        public CocktailDto ToDto(CocktailDbModel item)
        {
            var ingredientDbModels = _context.IngredientsSet.Where(e => e.CocktailId == item.Id).ToList();
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