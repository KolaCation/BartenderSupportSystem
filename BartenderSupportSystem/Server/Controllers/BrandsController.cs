using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BartenderSupportSystem.Server.Data;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;
using Microsoft.AspNetCore.Cors;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.RecommendationSystem;
using BartenderSupportSystem.Server.Data.Mappers.Implementation.RecommendationSystem;

namespace BartenderSupportSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class BrandsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IBrandMapper _brandMapper;

        public BrandsController(ApplicationDbContext context)
        {
            _context = context;
            _brandMapper = new BrandMapper();
        }

        //GET: api/Brands
        [HttpGet]
        public async Task<ActionResult<List<BrandDto>>> GetBrand()
        {
            var brandDbModels = await _context.BrandsSet.ToListAsync();
            var brands = (from brandDbModel in brandDbModels select _brandMapper.ToDto(brandDbModel)).ToList();
            return brands;
        }

        // GET: api/Brands/5
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

        // PUT: api/Brands/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(int id, BrandDto brand)
        {
            if (!id.Equals(brand.Id))
            {
                return BadRequest();
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

        // POST: api/Brands
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BrandDto>> PostBrand(BrandDto brand)
        {
            var brandDbModel = _brandMapper.ToDbModel(brand);
            await _context.BrandsSet.AddAsync(brandDbModel);
            await _context.SaveChangesAsync();
            var createdBrand = _context.BrandsSet.OrderByDescending(e => e.Id).FirstOrDefault();

            return CreatedAtAction(nameof(GetBrand), new { id = createdBrand.Id }, _brandMapper.ToDto(createdBrand));
        }

        // DELETE: api/Brands/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brandDbModel = await _context.BrandsSet.FindAsync(id);
            if (brandDbModel == null)
            {
                return NotFound();
            }

            _context.BrandsSet.Remove(brandDbModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BrandExists(int id)
        {
            return _context.BrandsSet.Any(e => e.Id.Equals(id));
        }
    }
}
