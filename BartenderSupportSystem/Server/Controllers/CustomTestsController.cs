using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BartenderSupportSystem.Server.Data;
using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Implementation.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem;
using BartenderSupportSystem.Shared.Models.TestSystem;
using Microsoft.AspNetCore.Cors;

namespace BartenderSupportSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class CustomTestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomAnswerMapper _customAnswerMapper;
        private readonly ICustomQuestionMapper _customQuestionMapper;
        private readonly ICustomTestMapper _customTestMapper;

        public CustomTestsController(ApplicationDbContext context)
        {
            _context = context;
            _customAnswerMapper = new CustomAnswerMapper();
            _customQuestionMapper = new CustomQuestionMapper();
            _customTestMapper = new CustomTestMapper();
        }

        // GET: api/CustomTests
        [HttpGet]
        public async Task<ActionResult<List<CustomTestDto>>> GetCustomTest()
        {
            var testDbModels = await _context.TestsSet.ToListAsync();
            var tests = (from testDbModel in testDbModels select _customTestMapper.ToDto(testDbModel))
                .ToList();
            return tests;
        }

        // GET: api/CustomTests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomTestDto>> GetCustomTest(int id)
        {
            var customTestDbModel = await _context.TestsSet.FindAsync(id);

            if (customTestDbModel == null)
            {
                return NotFound();
            }

            var test = _customTestMapper.ToDto(customTestDbModel);
            return test;
        }

        // PUT: api/CustomTests/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomTest(int id, CustomTestDto customTest)
        {
            if (!id.Equals(customTest.Id))
            {
                return BadRequest();
            }

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            _context.Entry(customTest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomTestExists(id))
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

        // POST: api/CustomTests
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CustomTestDto>> PostCustomTest(CustomTestDto customTest)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            var customTestDbModel = _customTestMapper.ToDbModel(customTest);
            await _context.TestsSet.AddAsync(customTestDbModel);
            await _context.SaveChangesAsync();
            var createdCustomTest = _context.CocktailsSet.OrderByDescending(e => e.Id).First();

            return CreatedAtAction(nameof(GetCustomTest), new { id = createdCustomTest.Id }, createdCustomTest);
        }

        // DELETE: api/CustomTests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomTest(int id)
        {
            var customTestDbModel = await _context.TestsSet.FindAsync(id);
            if (customTestDbModel == null)
            {
                return NotFound();
            }

            _context.TestsSet.Remove(customTestDbModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomTestExists(int id)
        {
            return _context.TestsSet.Any(e => e.Id.Equals(id));
        }
    }
}
