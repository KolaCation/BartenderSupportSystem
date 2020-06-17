using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BartenderSupportSystem.Server.DbModels.TestSystem
{
    internal sealed class CustomAnswerDbModel
    {
        public Guid Id { get; private set; }
        public string Statement { get; private set; }
        public bool IsCorrect { get; private set; }
        public Guid QuestionId { get; private set; }
        [ForeignKey("QuestionId")]
        public CustomQuestionDbModel Question { get; private set; }

        public CustomAnswerDbModel(Guid id, string statement, bool isCorrect, Guid questionId)
        {
            Id = id;
            Statement = statement;
            IsCorrect = isCorrect;
            QuestionId = questionId;
        }
    }
}
