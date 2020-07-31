using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BartenderSupportSystem.Server.Data;
using BartenderSupportSystem.Server.DomainServices.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Helpers;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;

        public DrinksController(ApplicationDbContext context, IMapper mapper, IStorageService storageService)
        {
            _context = context;
            _mapper = mapper;
            _storageService = storageService;
        }

        // GET: api/Drinks/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<DrinkDto>>> GetDrink()
        {
            var drinkDbModels = await _context.DrinksSet.ToListAsync();
            var drinks = _mapper.Map<List<DrinkDbModel>, List<DrinkDto>>(drinkDbModels);
            return drinks;
        }

        //GET: api/Drinks (paginated count)
        [HttpGet]
        public async Task<List<DrinkDto>> GetDrink([FromQuery] PaginationDto paginationDto)
        {
            var drinksQueryable = _context.DrinksSet.AsQueryable();
            await HttpContext.InsertPaginationParamsIntoResponse(drinksQueryable, paginationDto);
            var drinkDbModels = await drinksQueryable.InsertPagination(paginationDto).ToListAsync();
            var drinks = _mapper.Map<List<DrinkDbModel>, List<DrinkDto>>(drinkDbModels);
            return drinks;
        }

        // GET: api/Drinks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DrinkDto>> GetDrink(Guid id)
        {
            var drinkDbModel = await _context.DrinksSet.FindAsync(id);

            if (drinkDbModel == null)
            {
                return NotFound();
            }

            var drink = _mapper.Map<DrinkDbModel, DrinkDto>(drinkDbModel);
            return drink;
        }

        // PUT: api/Drinks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrink(Guid id, DrinkDto drink)
        {
            if (!id.Equals(drink.Id))
            {
                return BadRequest();
            }

            var drinkDbModelToUpdate = await _context.DrinksSet.FindAsync(id);
            if (drinkDbModelToUpdate == null)
            {
                return NotFound();
            }

            var fileRoute = drinkDbModelToUpdate.PhotoPath;
            drinkDbModelToUpdate = _mapper.Map<DrinkDto, DrinkDbModel>(drink);
            if (!string.IsNullOrEmpty(drink.PhotoPath))
            {
                drinkDbModelToUpdate.UpdatePhotoPath(await _storageService.EditFile(Convert.FromBase64String(drink.PhotoPath), "jpg", "drinks", fileRoute));
            }
            _context.Entry(drinkDbModelToUpdate).State = EntityState.Modified;

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

        // POST: api/Drinks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostDrink(DrinkDto drink)
        {
            if (!string.IsNullOrEmpty(drink.PhotoPath))
            {
                drink.PhotoPath = await _storageService.SaveFile(Convert.FromBase64String(drink.PhotoPath), "jpg", "drinks");
            }

            var drinkDbModel = _mapper.Map<DrinkDto, DrinkDbModel>(drink);
            await _context.DrinksSet.AddAsync(drinkDbModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Drinks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrink(Guid id)
        {
            var drinkDbModel = await _context.DrinksSet.FindAsync(id);
            if (drinkDbModel == null)
            {
                return NotFound();
            }

            _context.DrinksSet.Remove(drinkDbModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DrinkExists(Guid id)
        {
            return _context.DrinksSet.Any(e => e.Id.Equals(id));
        }
    }
}
