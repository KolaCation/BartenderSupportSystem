using System;
using System.Collections.Generic;
using System.Text;

namespace BartenderSupportSystem.Domain.TestSystem
{
    public sealed class CustomQuestion
    {
        public Guid Id { get; }
        public string Statement { get; }
        public Guid TestId { get; }
        private readonly List<CustomAnswer> _answers;
        public IReadOnlyCollection<CustomAnswer> Answers => _answers.AsReadOnly();

        public CustomQuestion(Guid id, string statement, Guid testId)
        {
            Id = id;
            Statement = statement;
            TestId = testId;
            _answers = new List<CustomAnswer>();
        }

        public CustomQuestion(Guid id, string statement, Guid testId, List<CustomAnswer> answers)
            : this(id, statement, testId)
        {
            _answers = answers;
        }
    }
}
