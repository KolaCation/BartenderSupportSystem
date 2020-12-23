using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BartenderSupportSystem.Server.Data;
using BartenderSupportSystem.Server.Data.Mappers.Implementation.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem;
using BartenderSupportSystem.Shared.Models.TestSystem;
using Microsoft.AspNetCore.Cors;

namespace BartenderSupportSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class RatingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IRatingMapper _ratingMapper;

        public RatingController(ApplicationDbContext context)
        {
            _context = context;
            _ratingMapper = new RatingMapper();
        }

        // GET: api/Rating
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingDto>>> GetRating()
        {
            var ratingDbModels = await _context.RatingsSet.Include(e => e.UserRatings).ToListAsync();
            var ratings = (from ratingDbModel in ratingDbModels select _ratingMapper.ToDto(ratingDbModel)).ToList();
            return ratings;
        }

        // GET: api/Rating/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RatingDto>> GetRating(int id)
        {
            var ratingDbModel = await _context.RatingsSet.Where(e => e.Id.Equals(id)).Include(e => e.UserRatings)
                .FirstOrDefaultAsync();

            if (ratingDbModel == null)
            {
                return NotFound();
            }

            var rating = _ratingMapper.ToDto(ratingDbModel);
            return rating;
        }

        // PUT: api/Rating/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRating(int id, RatingDto rating)
        {
            if (!id.Equals(rating.Id))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ratingDbModel = _ratingMapper.ToDbModel(rating);
            _context.Entry(ratingDbModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RatingDtoExists(id))
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

        // POST: api/Rating
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<RatingDto>> PostRating(RatingDto rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ratingDbModel = _ratingMapper.ToDbModel(rating);
            await _context.RatingsSet.AddAsync(ratingDbModel);
            await _context.SaveChangesAsync();
            var createdRating = _context.RatingsSet.OrderByDescending(e => e.Id).Include(e => e.UserRatings).First();

            return CreatedAtAction("GetRating", new {id = createdRating.Id}, _ratingMapper.ToDto(createdRating));
        }

        // DELETE: api/Rating/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRating(int id)
        {
            var ratingDbModel = await _context.RatingsSet.FindAsync(id);
            if (ratingDbModel == null)
            {
                return NotFound();
            }

            _context.RatingsSet.Remove(ratingDbModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RatingDtoExists(int id)
        {
            return _context.RatingsSet.Any(e => e.Id.Equals(id));
        }
    }
}