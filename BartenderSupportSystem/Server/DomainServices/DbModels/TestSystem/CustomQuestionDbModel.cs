using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BartenderSupportSystem.Server.DomainServices.DbModels.TestSystem
{
    internal sealed class CustomQuestionDbModel
    {
        public Guid Id { get; private set; }
        public string Statement { get; private set; }
        public Guid TestId { get; private set; }
        [ForeignKey("TestId")]
        public CustomTestDbModel Test { get; private set; }
        public List<CustomAnswerDbModel> Answers { get; private set; }


        public CustomQuestionDbModel(Guid id, string statement, Guid testId)
        {
            Id = id;
            Statement = statement;
            TestId = testId;
            Answers = new List<CustomAnswerDbModel>();
        }
    }
}
