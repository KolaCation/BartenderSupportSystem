using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BartenderSupportSystem.Server.Data;
using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Data.Mappers.Implementation.RecommendationSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem;
using BartenderSupportSystem.Server.Helpers;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using BartenderSupportSystem.Shared.Utils;
using Microsoft.AspNetCore.Cors;

namespace BartenderSupportSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class IngredientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IIngredientMapper _ingredientMapper;

        public IngredientsController(ApplicationDbContext context)
        {
            _context = context;
            _ingredientMapper = new IngredientMapper(context);
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<ActionResult<List<IngredientDto>>> GetIngredient()
        {
            var ingredientDbModels = await _context.IngredientsSet.ToListAsync();
            var ingredients = (from ingredientDbModel in ingredientDbModels select _ingredientMapper.ToDto(ingredientDbModel)).ToList();
            return ingredients;
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientDto>> GetIngredient(int id)
        {
            var ingredientDbModel = await _context.IngredientsSet.FindAsync(id);

            if (ingredientDbModel == null)
            {
                return NotFound();
            }

            var ingredient = _ingredientMapper.ToDto(ingredientDbModel);
            return ingredient;
        }

        // PUT: api/Ingredients/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredient(int id, IngredientDto ingredient)
        {
            if (!id.Equals(ingredient.Id))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ingredientDbModel = _ingredientMapper.ToDbModel(ingredient);
            _context.Entry(ingredientDbModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(id))
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

        // POST: api/Ingredients
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<IngredientDto>> PostIngredient(IngredientDto ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ingredientDbModel = _ingredientMapper.ToDbModel(ingredient);
            await _context.IngredientsSet.AddAsync(ingredientDbModel);
            await _context.SaveChangesAsync();
            var createdIngredient = _context.IngredientsSet.OrderByDescending(e => e.Id).First();

            return CreatedAtAction(nameof(GetIngredient), new { id = createdIngredient.Id }, _ingredientMapper.ToDto(createdIngredient));
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(Guid id)
        {
            var ingredientDbModel = await _context.IngredientsSet.FindAsync(id);
            if (ingredientDbModel == null)
            {
                return NotFound();
            }

            _context.IngredientsSet.Remove(ingredientDbModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredientExists(int id)
        {
            return _context.IngredientsSet.Any(e => e.Id.Equals(id));
        }
    }
}
