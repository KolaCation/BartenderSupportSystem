using BartenderSupportSystem.Server.Data;
using BartenderSupportSystem.Server.Data.Mappers.Implementation.RecommendationSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem;
using BartenderSupportSystem.Server.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Server.Data.DTO.RecommendationSystem;

namespace BartenderSupportSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class BrandsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStorageService _storageService;
        private readonly IBrandMapper _brandMapper;

        public BrandsController(ApplicationDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
            _brandMapper = new BrandMapper();
        }

        [HttpGet]
        public async Task<ActionResult<List<BrandDto>>> GetBrand()
        {
            var brandDbModels = await _context.BrandsSet.ToListAsync();
            var brands = brandDbModels.Select(_brandMapper.ToDto).ToList();
            return brands;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BrandDto>> GetBrand(int id)
        {
            var brandDbModel = await _context.BrandsSet.FindAsync(id);

            if (brandDbModel == null)
            {
                return NotFound();
            }

            var brand = _brandMapper.ToDto(brandDbModel);
            return brand;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, BrandDto brand)
        {
            if (id != brand.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brandDbModel = _brandMapper.ToDbModel(brand);
            _context.Entry(brandDbModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrandExists(id))
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
        public async Task<ActionResult<BrandDto>> PostBrand(BrandDto brand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var brandDbModel = _brandMapper.ToDbModel(brand);
            await _context.BrandsSet.AddAsync(brandDbModel);
            await _context.SaveChangesAsync();
            var createdBrand = _context.BrandsSet.OrderByDescending(e => e.Id).First();

            return CreatedAtAction(nameof(GetBrand), new { id = createdBrand.Id }, _brandMapper.ToDto(createdBrand));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brandDbModel = await _context.BrandsSet.FindAsync(id);
            if (brandDbModel == null)
            {
                return NotFound();
            }

            _context.BrandsSet.Remove(brandDbModel);
            var drinksToRemove = await _context.DrinksSet.Where(e => e.BrandId == brandDbModel.Id).ToListAsync();
            foreach (var drinkDbModel in drinksToRemove)
            {
                await _storageService.DeleteFile(drinkDbModel.PhotoPath, "drinks");
            }

            _context.DrinksSet.RemoveRange(drinksToRemove);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BrandExists(int id)
        {
            return _context.BrandsSet.Any(e => e.Id == id);
        }
    }
}