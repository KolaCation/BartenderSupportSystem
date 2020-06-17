using System;
using System.Collections.Generic;

namespace BartenderSupportSystem.Server.DbModels.TestSystem
{
    internal sealed class CustomTestDbModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Topic { get; private set; }
        public string Description { get; private set; }
        public List<CustomQuestionDbModel> Questions { get; private set; }

        public CustomTestDbModel(Guid id, string name, string topic, string description)
        {
            Id = id;
            Name = name;
            Topic = topic;
            Description = description;
            Questions = new List<CustomQuestionDbModel>();
        }
    }
}
