using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BartenderSupportSystem.Server.Data.DbModels.TestSystem
{
    internal sealed class CustomAnswerDbModel
    {
        public int Id { get; private set; }
        public string Statement { get; private set; }
        public bool IsCorrect { get; private set; }
        public int QuestionId { get; private set; }
        [ForeignKey("QuestionId")]
        public CustomQuestionDbModel Question { get; private set; }

        public CustomAnswerDbModel(int id, string statement, bool isCorrect, int questionId)
        {
            Id = id;
            Statement = statement;
            IsCorrect = isCorrect;
            QuestionId = questionId;
        }
    }
}
