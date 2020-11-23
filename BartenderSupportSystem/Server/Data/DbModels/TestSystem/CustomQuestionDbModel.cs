using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Data.DbModels.TestSystem
{
    internal sealed class CustomQuestionDbModel
    {
        public int Id { get; private set; }
        public string Statement { get; private set; }
        public int TestId { get; private set; }
        [ForeignKey("TestId")] public CustomTestDbModel Test { get; private set; }
        public List<CustomAnswerDbModel> Answers { get; private set; }


        public CustomQuestionDbModel(string statement, int testId)
        {
            Statement = statement;
            TestId = testId;
            Answers = new List<CustomAnswerDbModel>();
        }

        public CustomQuestionDbModel(List<CustomAnswerDbModel> answers, string statement, int testId) : this(statement, testId)
        {
            CustomValidator.ValidateObject(answers);
            Answers = answers;
        }

        public CustomQuestionDbModel(int id, string statement, int testId) : this(statement, testId)
        {
            Id = id;
            Answers = new List<CustomAnswerDbModel>();
        }

        public CustomQuestionDbModel(List<CustomAnswerDbModel> answers, int id, string statement, int testId) : this(id, statement, testId)
        {
            CustomValidator.ValidateObject(answers);
            Answers = answers;
        }


    }
}