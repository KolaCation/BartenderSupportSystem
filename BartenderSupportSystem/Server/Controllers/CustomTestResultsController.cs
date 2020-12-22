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

namespace BartenderSupportSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomTestResultsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomTestResultMapper _customTestResultMapper;

        public CustomTestResultsController(ApplicationDbContext context)
        {
            _context = context;
            _customTestResultMapper = new CustomTestResultMapper();
        }

        // GET: api/CustomTestResults
        [HttpGet]
        public async Task<ActionResult<List<CustomTestResultDto>>> GetCustomTestResult([FromQuery] string username,
            [FromQuery] string testId)
        {
            var listToReturn = new List<CustomTestResultDto>();
            if (int.TryParse(testId, out var result) && username != null)
            {
                var customTestResultDbModel = await _context.TestResultsSet.Where(e =>
                        e.CustomTestId.Equals(result) && e.UserName.ToLower().Equals(username.ToLower()))
                    .Include(e => e.PickedAnswers)
                    .FirstOrDefaultAsync();
                if (customTestResultDbModel == null)
                {
                    return NotFound();
                }
                else
                {
                    var testResult = _customTestResultMapper.ToDto(customTestResultDbModel);
                    listToReturn.Add(testResult);
                }
            }
            else if (testId == null && username != null)
            {
                var testResultDbModels = await _context.TestResultsSet
                    .Where(e => e.UserName.ToLower().Equals(username.ToLower()))
                    .Include(e => e.PickedAnswers).ToListAsync();
                var testResults = (from customTestResultDbModel in testResultDbModels
                    select _customTestResultMapper.ToDto(customTestResultDbModel)).ToList();
                listToReturn.AddRange(testResults);
            }
            else
            {
                return BadRequest();
            }

            return listToReturn;
        }

        // PUT: api/CustomTestResults/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomTestResult(int id,
            CustomTestResultDto customTestResult)
        {
            if (User?.Identity.Name == null || !string.Equals(User.Identity.Name, customTestResult.UserName,
                StringComparison.OrdinalIgnoreCase))
            {
                return Unauthorized();
            }

            if (id != customTestResult.Id)
            {
                return BadRequest();
            }

            _context.Entry(customTestResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomTestResultDbModelExists(id))
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

        // POST: api/CustomTestResults
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CustomTestResultDto>> PostCustomTestResult(CustomTestResultDto customTestResult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customTestResultDbModel = _customTestResultMapper.ToDbModel(customTestResult);
            await _context.TestResultsSet.AddAsync(customTestResultDbModel);
            await _context.SaveChangesAsync();
            var createdTestResult = _context.TestResultsSet.OrderByDescending(e => e.Id).First();

            return CreatedAtAction(nameof(GetCustomTestResult), new {id = createdTestResult.Id},
                createdTestResult);
        }

        // DELETE: api/CustomTestResults/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IActionResult>> DeleteCustomTestResult(int id)
        {
            var customTestResultDbModel = await _context.TestResultsSet.Where(e => e.Id.Equals(id))
                .Include(e => e.PickedAnswers).FirstOrDefaultAsync();
            if (customTestResultDbModel == null)
            {
                return NotFound();
            }

            _context.TestResultsSet.Remove(customTestResultDbModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomTestResultDbModelExists(int id)
        {
            return _context.TestResultsSet.Any(e => e.Id.Equals(id));
        }
    }
}