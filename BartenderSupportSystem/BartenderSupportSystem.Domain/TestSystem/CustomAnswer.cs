using System;
using System.Collections.Generic;
using System.Text;

namespace BartenderSupportSystem.Domain.TestSystem
{
    public sealed class CustomAnswer
    {
        public Guid Id { get; private set; }
        public string Statement { get; private set; }
        public bool IsCorrect { get; private set; }
        public Guid QuestionId { get; private set; }

        public CustomAnswer(Guid id, string statement, bool isCorrect, Guid questionId)
        {
            Id = id;
            Statement = statement;
            IsCorrect = isCorrect;
            QuestionId = questionId;
        }
    }
}
