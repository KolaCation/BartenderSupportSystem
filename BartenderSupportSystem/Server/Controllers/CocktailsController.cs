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

namespace BartenderSupportSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CocktailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICocktailMapper _cocktailMapper;
        private readonly IStorageService _storageService;

        public CocktailsController(ApplicationDbContext context, IStorageService storageService)
        {
            _context = context;
            _cocktailMapper = new CocktailMapper();
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

            var cocktailDbModelToUpdate = await _context.CocktailsSet.FindAsync(id);
            if (cocktailDbModelToUpdate == null)
            {
                return NotFound();
            }
            _context.Entry(cocktailDbModelToUpdate).State = EntityState.Detached;
            var fileRoute = cocktailDbModelToUpdate.PhotoPath;
            cocktailDbModelToUpdate = _cocktailMapper.ToDbModel(cocktail);
            if (!string.IsNullOrEmpty(cocktail.PhotoPath))
            {
                cocktailDbModelToUpdate.UpdatePhotoPath(await _storageService.EditFile(Convert.FromBase64String(PhotoPathHelper.GetBase64String(cocktail.PhotoPath)), "jpg", "cocktails", fileRoute));
            }
            else
            {
                cocktailDbModelToUpdate.UpdatePhotoPath(fileRoute);
            }

            _context.Entry(cocktailDbModelToUpdate).State = EntityState.Modified;

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
                cocktail.PhotoPath = await _storageService.SaveFile(Convert.FromBase64String(PhotoPathHelper.GetBase64String(cocktail.PhotoPath)), "jpg", "cocktails");
            }
            var cocktailDbModel = _cocktailMapper.ToDbModel(cocktail);
            await _context.CocktailsSet.AddAsync(cocktailDbModel);
            await _context.SaveChangesAsync();
            var createdCocktail = _context.CocktailsSet.OrderByDescending(e => e.Id).First();

            return CreatedAtAction(nameof(GetCocktail), new { id = createdCocktail.Id }, _cocktailMapper.ToDto(createdCocktail));
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

            _context.CocktailsSet.Remove(cocktailDbModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CocktailExists(int id)
        {
            return _context.CocktailsSet.Any(e => e.Id.Equals(id));
        }
    }
}
