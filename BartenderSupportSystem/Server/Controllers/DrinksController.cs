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
using BartenderSupportSystem.Server.Data.DTO.RecommendationSystem.Enums;

namespace BartenderSupportSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class DrinksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStorageService _storageService;
        private readonly IDrinkMapper _drinkMapper;

        public DrinksController(ApplicationDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
            _drinkMapper = new DrinkMapper();
        }

        [HttpGet]
        public async Task<ActionResult<List<DrinkDto>>> GetDrink()
        {
            var drinkDbModels = await _context.DrinksSet.ToListAsync();
            var drinks = drinkDbModels.Select(_drinkMapper.ToDto).ToList();
            return drinks;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DrinkDto>> GetDrink(int id)
        {
            var drinkDbModel = await _context.DrinksSet.FindAsync(id);

            if (drinkDbModel == null)
            {
                return NotFound();
            }

            var drink = _drinkMapper.ToDto(drinkDbModel);
            return drink;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrink(int id, DrinkDto drink)
        {
            if (id != drink.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!DrinkExists(id))
            {
                return NotFound();
            }

            var updateDrinkSucceed = await TryUpdateDrink(drink);
            if (!updateDrinkSucceed)
            {
                return BadRequest();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrinkExists(id))
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
        public async Task<ActionResult<DrinkDto>> PostDrink(DrinkDto drink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!string.IsNullOrEmpty(drink.PhotoPath))
            {
                drink.PhotoPath = await _storageService.SaveFile(
                    Convert.FromBase64String(PhotoPathHelper.GetBase64String(drink.PhotoPath)), "jpg", "drinks");
            }

            var drinkDbModel = _drinkMapper.ToDbModel(drink);
            await _context.DrinksSet.AddAsync(drinkDbModel);
            await _context.SaveChangesAsync();
            var createdDrink = _context.DrinksSet.OrderByDescending(e => e.Id).First();

            return CreatedAtAction(nameof(GetDrink), new {id = createdDrink.Id}, _drinkMapper.ToDto(createdDrink));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrink(int id)
        {
            var drinkDbModel = await _context.DrinksSet.FindAsync(id);
            if (drinkDbModel == null)
            {
                return NotFound();
            }

            var ingredientsToRemove = await _context.IngredientsSet
                .Where(e => e.ComponentId.Equals(drinkDbModel.Id) && e.ProportionType == ProportionType.Milliliter)
                .ToListAsync();
            _context.IngredientsSet.RemoveRange(ingredientsToRemove);
            _context.DrinksSet.Remove(drinkDbModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DrinkExists(int id)
        {
            return _context.DrinksSet.Any(e => e.Id == id);
        }

        private async Task<bool> TryUpdateDrink(DrinkDto drink)
        {
            try
            {
                var drinkDbModelToUpdate = await _context.DrinksSet.FindAsync(drink.Id);
                _context.Entry(drinkDbModelToUpdate).State = EntityState.Detached;
                var fileRoute = drinkDbModelToUpdate.PhotoPath;
                drinkDbModelToUpdate = _drinkMapper.ToDbModel(drink);
                if (!string.IsNullOrEmpty(drink.PhotoPath))
                {
                    drinkDbModelToUpdate.PhotoPath = (await _storageService.EditFile(
                        Convert.FromBase64String(PhotoPathHelper.GetBase64String(drink.PhotoPath)), "jpg", "drinks",
                        fileRoute));
                }
                else
                {
                    drinkDbModelToUpdate.PhotoPath = fileRoute;
                }

                _context.Entry(drinkDbModelToUpdate).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}