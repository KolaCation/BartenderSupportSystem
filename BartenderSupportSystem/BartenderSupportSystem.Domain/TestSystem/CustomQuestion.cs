using System;
using System.Collections.Generic;
using System.Text;

namespace BartenderSupportSystem.Domain.TestSystem
{
    public sealed class CustomQuestion
    {
        public Guid Id { get; private set; }
        public string Statement { get; private set; }
        public Guid TestId { get; private set; }
        public IReadOnlyCollection<CustomAnswer> Answers { get; private set; }

        public CustomQuestion(Guid id, string statement, Guid testId)
        {
            Id = id;
            Statement = statement;
            TestId = testId;
        }
    }
}