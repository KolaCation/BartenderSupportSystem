using BartenderSupportSystem.Server.Data;
using BartenderSupportSystem.Server.Data.Mappers.Implementation.RecommendationSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using BartenderSupportSystem.Shared.Models.RecommendationSystem.Enums;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class MealsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMealMapper _mealMapper;

        public MealsController(ApplicationDbContext context)
        {
            _context = context;
            _mealMapper = new MealMapper();
        }

        // GET: api/Meals
        [HttpGet]
        public async Task<ActionResult<List<MealDto>>> GetMeal()
        {
            var mealDbModels = await _context.MealsSet.ToListAsync();
            var meals = (from mealDbModel in mealDbModels select _mealMapper.ToDto(mealDbModel)).ToList();
            return meals;
        }

        // GET: api/Meals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MealDto>> GetMeal(int id)
        {
            var mealDbModel = await _context.MealsSet.FindAsync(id);

            if (mealDbModel == null)
            {
                return NotFound();
            }

            var meal = _mealMapper.ToDto(mealDbModel);
            return meal;
        }

        // PUT: api/Meals/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeal(int id, MealDto meal)
        {
            if (!id.Equals(meal.Id))
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mealDbModel = _mealMapper.ToDbModel(meal);
            _context.Entry(mealDbModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealExists(id))
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

        // POST: api/Meals
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostMeal(MealDto meal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var mealDbModel = _mealMapper.ToDbModel(meal);
            await _context.MealsSet.AddAsync(mealDbModel);
            await _context.SaveChangesAsync();
            var createdMeal = _context.MealsSet.OrderByDescending(e => e.Id).First();

            return CreatedAtAction(nameof(GetMeal), new { id = createdMeal.Id }, _mealMapper.ToDto(createdMeal));
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal(int id)
        {
            var mealDbModel = await _context.MealsSet.FindAsync(id);
            if (mealDbModel == null)
            {
                return NotFound();
            }
            var ingredientsToRemove = await _context.IngredientsSet
                .Where(e => e.ComponentId.Equals(mealDbModel.Id) && e.ProportionType != ProportionType.Milliliter)
                .ToListAsync();
            _context.IngredientsSet.RemoveRange(ingredientsToRemove);
            _context.MealsSet.Remove(mealDbModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MealExists(int id)
        {
            return _context.MealsSet.Any(e => e.Id.Equals(id));
        }
    }
}
