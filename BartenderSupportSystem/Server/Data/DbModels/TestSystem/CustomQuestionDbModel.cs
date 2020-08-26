using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BartenderSupportSystem.Server.Data.DbModels.TestSystem
{
    internal sealed class CustomQuestionDbModel
    {
        public int Id { get; private set; }
        public string Statement { get; private set; }
        public int TestId { get; private set; }
        [ForeignKey("TestId")]
        public CustomTestDbModel Test { get; private set; }
        public List<CustomAnswerDbModel> Answers { get; private set; }


        public CustomQuestionDbModel(int id, string statement, int testId)
        {
            Id = id;
            Statement = statement;
            TestId = testId;
            Answers = new List<CustomAnswerDbModel>();
        }
    }
}
