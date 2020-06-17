using System;
using System.Collections.Generic;
using System.Text;

namespace BartenderSupportSystem.Domain.TestSystem
{
    public sealed class CustomAnswer
    {
        public Guid Id { get; }
        public string Statement { get; }
        public bool IsCorrect { get; }
        public Guid QuestionId { get; }

        public CustomAnswer(Guid id, string statement, bool isCorrect, Guid questionId)
        {
            Id = id;
            Statement = statement;
            IsCorrect = isCorrect;
            QuestionId = questionId;
        }
    }
}
