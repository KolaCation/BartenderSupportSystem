using BartenderSupportSystem.Server.Data;
using BartenderSupportSystem.Server.Data.Mappers.Implementation.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem;
using Microsoft.AspNetCore.Cors;
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
    [EnableCors("CorsPolicy")]
    public class CustomTestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomTestMapper _customTestMapper;

        public CustomTestsController(ApplicationDbContext context)
        {
            _context = context;
            _customTestMapper = new CustomTestMapper();
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomTestDto>>> GetCustomTest()
        {
            var testDbModels =
                await _context.TestsSet.Include(e => e.Questions).ThenInclude(e => e.Answers).ToListAsync();
            var tests = testDbModels.Select(_customTestMapper.ToDto).ToList();
            return tests;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomTestDto>> GetCustomTest(int id)
        {
            var customTestDbModel = await _context.TestsSet.Where(e => e.Id == id).Include(e => e.Questions)
                .ThenInclude(e => e.Answers).FirstOrDefaultAsync();

            if (customTestDbModel == null)
            {
                return NotFound();
            }

            var test = _customTestMapper.ToDto(customTestDbModel);
            return test;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomTest(int id, CustomTestDto customTest)
        {
            if (User?.Identity.Name == null || !string.Equals(User.Identity.Name, customTest.AuthorUsername, StringComparison.OrdinalIgnoreCase))
            {
                return Unauthorized();
            }

            if (id != customTest.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateSucceed = await TryUpdateCustomTest(customTest);
            if (!updateSucceed)
            {
                return BadRequest();
            }

            var removeSucceed = await TryRemoveCustomTestResults(customTest.Id);
            if (!removeSucceed)
            {
                return BadRequest();
            }

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

        [HttpPost]
        public async Task<ActionResult<CustomTestDto>> PostCustomTest(CustomTestDto customTest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customTestDbModel = _customTestMapper.ToDbModel(customTest);
            await _context.TestsSet.AddAsync(customTestDbModel);
            await _context.SaveChangesAsync();
            var createdCustomTest = _context.TestsSet.OrderByDescending(e => e.Id).First();

            return CreatedAtAction(nameof(GetCustomTest), new { id = createdCustomTest.Id }, _customTestMapper.ToDto(createdCustomTest));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomTest(int id)
        {
            var customTestDbModel = await _context.TestsSet.Where(e => e.Id == id).Include(e => e.Questions)
                .ThenInclude(e => e.Answers).FirstOrDefaultAsync();
            if (customTestDbModel == null)
            {
                return NotFound();
            }

            if (User?.Identity.Name == null || !string.Equals(User.Identity.Name, customTestDbModel.AuthorUsername,
                StringComparison.OrdinalIgnoreCase))
            {
                return Unauthorized();
            }

            var removeSucceed = await TryRemoveCustomTestResults(customTestDbModel.Id);
            if (!removeSucceed)
            {
                return BadRequest();
            }
            _context.TestsSet.Remove(customTestDbModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomTestExists(int id)
        {
            return _context.TestsSet.Any(e => e.Id == id);
        }

        private async Task<bool> TryUpdateCustomTest(CustomTestDto customTest)
        {
            try
            {
                var customTestDbModel = _customTestMapper.ToDbModel(customTest);

                var questionDbModelsToAdd = customTestDbModel.Questions.Where(e => e.Id == 0).ToList();
                if (questionDbModelsToAdd.Count != 0)
                {
                    await _context.QuestionsSet.AddRangeAsync(questionDbModelsToAdd);
                }

                var questionIdsList = await _context.QuestionsSet.Where(e => e.TestId == customTestDbModel.Id)
                    .Select(e => e.Id).ToListAsync();
                foreach (var questionId in questionIdsList)
                {
                    if (customTestDbModel.Questions.Any(e => e.Id == questionId))
                    {
                        var questionDbModelToUpdate =
                            customTestDbModel.Questions.First(e => e.Id == questionId);
                        var answerDbModelsToAdd = questionDbModelToUpdate.Answers
                            .Where(e => e.Id == 0 && e.QuestionId != 0)
                            .ToList();
                        if (answerDbModelsToAdd.Count != 0)
                        {
                            await _context.AnswersSet.AddRangeAsync(answerDbModelsToAdd);
                        }

                        var answerIdsList = await _context.AnswersSet
                            .Where(e => e.QuestionId == questionDbModelToUpdate.Id).Select(e => e.Id)
                            .ToListAsync();
                        foreach (var answerId in answerIdsList)
                        {
                            if (questionDbModelToUpdate.Answers.Any(e => e.Id == answerId))
                            {
                                var answerDbModelToUpdate =
                                    questionDbModelToUpdate.Answers.First(e => e.Id == answerId);
                                _context.Entry(answerDbModelToUpdate).State = EntityState.Modified;
                            }
                            else
                            {
                                var answerDbModelToRemove = await _context.AnswersSet.FindAsync(answerId);
                                _context.AnswersSet.Remove(answerDbModelToRemove);
                            }
                        }

                        _context.Entry(questionDbModelToUpdate).State = EntityState.Modified;
                    }
                    else
                    {
                        var questionDbModelToRemove = await _context.QuestionsSet.Where(e => e.Id == questionId)
                            .Include(e => e.Answers).FirstOrDefaultAsync();
                        _context.QuestionsSet.Remove(questionDbModelToRemove);
                    }
                }

                _context.Entry(customTestDbModel).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<bool> TryRemoveCustomTestResults(int customTestId)
        {
            try
            {
                var testResultsToRemove =
                    await _context.TestResultsSet.Where(e => e.CustomTestId == customTestId).ToListAsync();
                _context.TestResultsSet.RemoveRange(testResultsToRemove);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}