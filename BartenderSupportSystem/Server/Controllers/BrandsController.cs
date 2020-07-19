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
    public class BrandsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BrandsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Brands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> GetBrand()
        {
            var brandDbModels = await _context.BrandsSet.ToListAsync();
            var brands = _mapper.Map<List<BrandDbModel>, List<Brand>>(brandDbModels);
            return brands;
        }
        /*
        //GET: api/Brands (paginated count)
        [HttpGet]
        public async Task<List<Brand>> GetBrand([FromQuery] PaginationDto paginationDto)
        {
            var brandsQueryable = _context.BrandsSet.AsQueryable();
            await HttpContext.InsertPaginationParamsIntoResponse(brandsQueryable, paginationDto);
            var brandDbModels = await brandsQueryable.InsertPagination(paginationDto).ToListAsync();
            var brands = _mapper.Map<List<BrandDbModel>, List<Brand>>(brandDbModels);
            return brands;
        }
        */
        // GET: api/Brands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(Guid id)
        {
            var brandDbModel = await _context.BrandsSet.FindAsync(id);

            if (brandDbModel == null)
            {
                return NotFound();
            }

            var brand = _mapper.Map<BrandDbModel, Brand>(brandDbModel);
            return brand;
        }

        // PUT: api/Brands/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrand(Guid id, Brand brand)
        {
            if (!id.Equals(brand.Id))
            {
                return BadRequest();
            }

            var brandDbModel = _mapper.Map<Brand, BrandDbModel>(brand);
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
        public async Task<ActionResult<Brand>> PostBrand(Brand brand)
        {
            var brandDbModel = _mapper.Map<Brand, BrandDbModel>(brand);
            await _context.BrandsSet.AddAsync(brandDbModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Brands/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Brand>> DeleteBrand(Guid id)
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

        private bool BrandExists(Guid id)
        {
            return _context.BrandsSet.Any(e => e.Id.Equals(id));
        }
    }
}
