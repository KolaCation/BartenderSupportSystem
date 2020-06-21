using System;
using System.Collections.Generic;
using System.Text;

namespace BartenderSupportSystem.Domain.TestSystem
{
    public sealed class CustomTest
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Topic { get; private set; }
        public string Description { get; private set; }
        public IReadOnlyCollection<CustomQuestion> Questions { get; private set; }

        public CustomTest(Guid id, string name, string topic, string description)
        {
            Id = id;
            Name = name;
            Topic = topic;
            Description = description;
        }
    }
}
