using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Server.Data;
using BartenderSupportSystem.Shared.Models.TestSystem;
using FluentValidation;

namespace BartenderSupportSystem.Server.Validators.TestSystem
{
    public sealed class PickedAnswerValidator : AbstractValidator<PickedAnswerDto>
    {
        private readonly ApplicationDbContext _context;

        public PickedAnswerValidator(ApplicationDbContext context)
        {
            _context = context;
            RuleFor(e => e.CustomAnswerId).Must(ExistCustomAnswer);
        }

        private bool ExistCustomAnswer(int customAnswerId)
        {
            return _context.AnswersSet.Any(e => e.Id.Equals(customAnswerId));
        }
    }
}
