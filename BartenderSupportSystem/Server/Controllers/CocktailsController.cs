using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BartenderSupportSystem.Server.Data;
using BartenderSupportSystem.Server.Data.Mappers.Implementation.RecommendationSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem;
using BartenderSupportSystem.Server.Helpers;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using Microsoft.AspNetCore.Cors;

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

        // GET: api/Cocktails
        [HttpGet]
        public async Task<ActionResult<List<CocktailDto>>> GetCocktail()
        {
            var cocktailDbModels = await _context.CocktailsSet.ToListAsync();
            var cocktails = (from cocktailDbModel in cocktailDbModels select _cocktailMapper.ToDto(cocktailDbModel))
                .ToList();
            return cocktails;
        }

        // GET: api/Cocktails/5
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

        // PUT: api/Cocktails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCocktail(int id, CocktailDto cocktail)
        {
            if (!id.Equals(cocktail.Id))
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

        // POST: api/Cocktails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
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

            return CreatedAtAction(nameof(GetCocktail), new {id = createdCocktail.Id},
                _cocktailMapper.ToDto(createdCocktail));
        }

        // DELETE: api/Cocktails/5
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
            return _context.CocktailsSet.Any(e => e.Id.Equals(id));
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
                    cocktailDbModelToUpdate.UpdatePhotoPath(await _storageService.EditFile(
                        Convert.FromBase64String(PhotoPathHelper.GetBase64String(cocktail.PhotoPath)), "jpg",
                        "cocktails",
                        fileRoute));
                }
                else
                {
                    cocktailDbModelToUpdate.UpdatePhotoPath(fileRoute);
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
                var ingredientsDbCount =
                    await _context.IngredientsSet.Where(e => e.CocktailId.Equals(cocktail.Id)).CountAsync();

                if (cocktail.Ingredients != null && ingredientsDbCount != cocktail.Ingredients.Count)
                {
                    var ingredientDbModels = _ingredientMapper.ToDbModelList(cocktail.Ingredients);
                    foreach (var ingredientDbModel in ingredientDbModels)
                    {
                        if (ingredientDbModel.Id == 0)
                        {
                            await _context.IngredientsSet.AddAsync(ingredientDbModel);
                        }
                        else
                        {
                            _context.Entry(ingredientDbModel).State = EntityState.Modified;
                        }
                    }
                }
                else if (cocktail.Ingredients == null && ingredientsDbCount != 0)
                {
                    var ingredientDbModels =
                        _context.IngredientsSet.Where(e => e.CocktailId.Equals(cocktail.Id)).ToList();
                    _context.RemoveRange(ingredientDbModels);
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