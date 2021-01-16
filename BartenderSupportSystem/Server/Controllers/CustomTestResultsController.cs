using BartenderSupportSystem.Server.Data;
using BartenderSupportSystem.Server.Data.Mappers.Implementation.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Server.Data.DTO.TestSystem;

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

                if (customTestResultDbModel != null)
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
                var testResults = testResultDbModels.Select(_customTestResultMapper.ToDto).ToList();
                listToReturn.AddRange(testResults);
            }
            else
            {
                return BadRequest();
            }

            return listToReturn;
        }

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

            return CreatedAtAction(nameof(GetCustomTestResult), new { id = createdTestResult.Id },
                _customTestResultMapper.ToDto(createdTestResult));
        }
    }
}