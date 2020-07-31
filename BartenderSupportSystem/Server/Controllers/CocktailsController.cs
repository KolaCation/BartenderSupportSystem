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
    public class CocktailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStorageService _storageService;

        public CocktailsController(ApplicationDbContext context, IMapper mapper, IStorageService storageService)
        {
            _context = context;
            _mapper = mapper;
            _storageService = storageService;
        }

        // GET: api/Cocktails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CocktailDto>>> GetCocktail()
        {
            var cocktailDbModels = await _context.CocktailsSet.ToListAsync();
            var cocktails = _mapper.Map<List<CocktailDbModel>, List<CocktailDto>>(cocktailDbModels);
            return cocktails;
        }

        //GET: api/Cocktails (paginated count)
        [HttpGet]
        public async Task<List<CocktailDto>> GetCocktail([FromQuery] PaginationDto paginationDto)
        {
            var cocktailsQueryable = _context.CocktailsSet.AsQueryable();
            await HttpContext.InsertPaginationParamsIntoResponse(cocktailsQueryable, paginationDto);
            var cocktailDbModels = await cocktailsQueryable.InsertPagination(paginationDto).ToListAsync();
            var cocktails = _mapper.Map<List<CocktailDbModel>, List<CocktailDto>>(cocktailDbModels);
            return cocktails;
        }

        // GET: api/Cocktails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CocktailDto>> GetCocktail(Guid id)
        {
            var cocktailDbModel = await _context.CocktailsSet.FindAsync(id);

            if (cocktailDbModel == null)
            {
                return NotFound();
            }

            var cocktail = _mapper.Map<CocktailDbModel, CocktailDto>(cocktailDbModel);
            return cocktail;
        }

        // PUT: api/Cocktails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCocktail(Guid id, CocktailDto cocktail)
        {
            if (!id.Equals(cocktail.Id))
            {
                return BadRequest();
            }

            var cocktailDbModelToUpdate = await _context.CocktailsSet.FindAsync(id);
            if (cocktailDbModelToUpdate == null)
            {
                return NotFound();
            }

            var fileRoute = cocktailDbModelToUpdate.PhotoPath;
            cocktailDbModelToUpdate = _mapper.Map<CocktailDto, CocktailDbModel>(cocktail);
            if (!string.IsNullOrEmpty(cocktail.PhotoPath))
            {
                cocktailDbModelToUpdate.UpdatePhotoPath(await _storageService.EditFile(Convert.FromBase64String(cocktail.PhotoPath), "jpg", "cocktails", fileRoute));
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
        public async Task<IActionResult> PostCocktail(CocktailDto cocktail)
        {
            if(!string.IsNullOrEmpty(cocktail.PhotoPath))
            {
                cocktail.PhotoPath = await _storageService.SaveFile(Convert.FromBase64String(cocktail.PhotoPath), "jpg", "cocktails");
            }
            var cocktailDbModel = _mapper.Map<CocktailDto, CocktailDbModel>(cocktail);
            await _context.CocktailsSet.AddAsync(cocktailDbModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Cocktails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCocktail(Guid id)
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

        private bool CocktailExists(Guid id)
        {
            return _context.CocktailsSet.Any(e => e.Id.Equals(id));
        }
    }
}
