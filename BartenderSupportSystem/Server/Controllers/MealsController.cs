using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BartenderSupportSystem.Server.Data;
using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Helpers;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MealsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Meals
        [HttpGet]
        public async Task<ActionResult<List<MealDto>>> GetMeal()
        {
            var mealDbModels = await _context.MealsSet.ToListAsync();
            var meals = _mapper.Map<List<MealDbModel>, List<MealDto>>(mealDbModels);
            return meals;
        }

        // GET: api/Meals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MealDto>> GetMeal(Guid id)
        {
            var mealDbModel = await _context.MealsSet.FindAsync(id);

            if (mealDbModel == null)
            {
                return NotFound();
            }

            var meal = _mapper.Map<MealDbModel, MealDto>(mealDbModel);
            return meal;
        }

        // PUT: api/Meals/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeal(Guid id, MealDto meal)
        {
            if (!id.Equals(meal.Id))
            {
                return BadRequest();
            }

            var mealDbModel = _mapper.Map<MealDto, MenuDbModel>(meal);
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
            var mealDbModel = _mapper.Map<MealDto, MealDbModel>(meal);
            await _context.MealsSet.AddAsync(mealDbModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeal(Guid id)
        {
            var mealDbModel = await _context.MealsSet.FindAsync(id);
            if (mealDbModel == null)
            {
                return NotFound();
            }

            _context.MealsSet.Remove(mealDbModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MealExists(Guid id)
        {
            return _context.MealsSet.Any(e => e.Id.Equals(id));
        }
    }
}
