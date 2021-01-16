using BartenderSupportSystem.Server.Data;
using BartenderSupportSystem.Server.Data.Mappers.Implementation.RecommendationSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem;
using BartenderSupportSystem.Server.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Server.Data.DTO.RecommendationSystem;

namespace BartenderSupportSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class CocktailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICocktailMapper _cocktailMapper;
        private readonly IIngredientMapper _ingredientMapper;
        private readonly IStorageService _storageService;

        public CocktailsController(ApplicationDbContext context, IStorageService storageService)
        {
            _context = context;
            _cocktailMapper = new CocktailMapper(context);
            _ingredientMapper = new IngredientMapper(context);
            _storageService = storageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CocktailDto>>> GetCocktail()
        {
            var cocktailDbModels = await _context.CocktailsSet.ToListAsync();
            var cocktails = cocktailDbModels.Select(_cocktailMapper.ToDto).ToList();
            return cocktails;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CocktailDto>> GetCocktail(int id)
        {
            var cocktailDbModel = await _context.CocktailsSet.FindAsync(id);

            if (cocktailDbModel == null)
            {
                return NotFound();
            }

            var cocktail = _cocktailMapper.ToDto(cocktailDbModel);
            return cocktail;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCocktail(int id, CocktailDto cocktail)
        {
            if (id != cocktail.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!CocktailExists(id))
            {
                return NotFound();
            }

            var updateCocktailSucceed = await TryUpdateCocktail(cocktail);
            var updateCocktailIngredientsSucceed = await TryUpdateCocktailIngredients(cocktail);

            if (!updateCocktailSucceed || !updateCocktailIngredientsSucceed)
            {
                return BadRequest();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CocktailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CocktailDto>> PostCocktail(CocktailDto cocktail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!string.IsNullOrEmpty(cocktail.PhotoPath))
            {
                cocktail.PhotoPath = await _storageService.SaveFile(
                    Convert.FromBase64String(PhotoPathHelper.GetBase64String(cocktail.PhotoPath)), "jpg", "cocktails");
            }

            var cocktailDbModel = _cocktailMapper.ToDbModel(cocktail);
            await _context.CocktailsSet.AddAsync(cocktailDbModel);
            await _context.SaveChangesAsync();
            var createdCocktail = _context.CocktailsSet.OrderByDescending(e => e.Id).First();
            if (cocktail.Ingredients != null)
            {
                foreach (var ingredientDto in cocktail.Ingredients)
                {
                    ingredientDto.CocktailId = createdCocktail.Id;
                }

                var ingredientDbModels = _ingredientMapper.ToDbModelList(cocktail.Ingredients);
                await _context.IngredientsSet.AddRangeAsync(ingredientDbModels);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetCocktail), new { id = createdCocktail.Id },
                _cocktailMapper.ToDto(createdCocktail));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCocktail(int id)
        {
            var cocktailDbModel = await _context.CocktailsSet.FindAsync(id);
            if (cocktailDbModel == null)
            {
                return NotFound();
            }

            var cocktail = _cocktailMapper.ToDto(cocktailDbModel);
            if (cocktail.Ingredients != null)
            {
                var ingredientDbModels = _ingredientMapper.ToDbModelList(cocktail.Ingredients);
                _context.IngredientsSet.RemoveRange(ingredientDbModels);
            }

            _context.CocktailsSet.Remove(cocktailDbModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CocktailExists(int id)
        {
            return _context.CocktailsSet.Any(e => e.Id == id);
        }

        private async Task<bool> TryUpdateCocktail(CocktailDto cocktail)
        {
            try
            {
                var cocktailDbModelToUpdate = await _context.CocktailsSet.FindAsync(cocktail.Id);
                _context.Entry(cocktailDbModelToUpdate).State = EntityState.Detached;
                var fileRoute = cocktailDbModelToUpdate.PhotoPath;
                cocktailDbModelToUpdate = _cocktailMapper.ToDbModel(cocktail);
                if (!string.IsNullOrEmpty(cocktail.PhotoPath))
                {
                    cocktailDbModelToUpdate.PhotoPath = await _storageService.EditFile(
                        Convert.FromBase64String(PhotoPathHelper.GetBase64String(cocktail.PhotoPath)), "jpg",
                        "cocktails",
                        fileRoute);
                }
                else
                {
                    cocktailDbModelToUpdate.PhotoPath = (fileRoute);
                }

                _context.Entry(cocktailDbModelToUpdate).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<bool> TryUpdateCocktailIngredients(CocktailDto cocktail)
        {
            try
            {
                if (cocktail.Ingredients != null)
                {
                    var ingredientsToAdd = cocktail.Ingredients.Where(e => e.Id == 0).ToList();
                    if (ingredientsToAdd.Count != 0)
                    {
                        await _context.AddRangeAsync(_ingredientMapper.ToDbModelList(ingredientsToAdd));
                    }

                    var ingredientIdsList = await _context.IngredientsSet.Where(e => e.CocktailId == cocktail.Id)
                        .Select(e => e.Id).ToListAsync();

                    foreach (var ingredientId in ingredientIdsList)
                    {
                        if (cocktail.Ingredients.Any(e => e.Id == ingredientId))
                        {
                            var ingredientToUpdate = cocktail.Ingredients.First(e => e.Id == ingredientId);
                            _context.Entry(_ingredientMapper.ToDbModel(ingredientToUpdate)).State =
                                EntityState.Modified;
                        }
                        else
                        {
                            var ingredientToRemove = await _context.IngredientsSet.FindAsync(ingredientId);
                            _context.IngredientsSet.Remove(ingredientToRemove);
                        }
                    }
                }
                else
                {
                    var ingredientsToRemove = await _context.IngredientsSet.Where(e => e.CocktailId == cocktail.Id)
                        .ToListAsync();
                    _context.IngredientsSet.RemoveRange(ingredientsToRemove);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}