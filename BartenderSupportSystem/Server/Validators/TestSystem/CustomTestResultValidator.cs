using BartenderSupportSystem.Server.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BartenderSupportSystem.Server.Data.DTO.TestSystem;

namespace BartenderSupportSystem.Server.Validators.TestSystem
{
    public sealed class CustomTestResultValidator : AbstractValidator<CustomTestResultDto>
    {
        private readonly ApplicationDbContext _context;

        public CustomTestResultValidator(ApplicationDbContext context)
        {
            _context = context;
            RuleFor(e => e.UserName)
                .NotEmpty().WithMessage("Username is required.");
            RuleFor(e => e.CustomTestId).Must(ExistCustomTest).WithMessage("Test ID is not valid.");
            RuleFor(e => e.PersonalMark)
                .GreaterThanOrEqualTo(0).WithMessage("Min value: 0.")
                .LessThanOrEqualTo(100).WithMessage("Max value: 100.");
            RuleFor(e => e)
                .NotEmpty().WithMessage("Picked answers are required.")
                .Must(HaveSameAmountOfAnswersAsTest)
                .WithMessage("Invalid result: picked answers list does not have same amount of answers as origin test.");
            RuleForEach(e => e.PickedAnswers).SetValidator(new PickedAnswerValidator(context));
        }

        private bool ExistCustomTest(int customTestId)
        {
            return _context.TestsSet.Any(e => e.Id.Equals(customTestId));
        }

        private bool HaveSameAmountOfAnswersAsTest(CustomTestResultDto customTestResult)
        {
            var customTestDbModel = _context.TestsSet.Where(e => e.Id.Equals(customTestResult.CustomTestId))
                .Include(e => e.Questions)
                .ThenInclude(e => e.Answers).First();
            var totalAnswersCount = customTestDbModel.Questions.Sum(customQuestionDbModel => customQuestionDbModel.Answers.Count);
            return customTestResult.PickedAnswers.Count == totalAnswersCount;
        }
    }
}