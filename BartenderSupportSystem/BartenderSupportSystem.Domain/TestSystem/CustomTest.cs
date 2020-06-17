using System;
using System.Collections.Generic;
using System.Text;

namespace BartenderSupportSystem.Domain.TestSystem
{
    public sealed class CustomTest
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Topic { get; }
        public string Description { get; }
        private readonly List<CustomQuestion> _questions;
        public IReadOnlyCollection<CustomQuestion> Questions => _questions.AsReadOnly();

        public CustomTest(Guid id, string name, string topic, string description)
        {
            Id = id;
            Name = name;
            Topic = topic;
            Description = description;
            _questions = new List<CustomQuestion>();
        }

        public CustomTest(Guid id, string name, string topic, string description, List<CustomQuestion> questions)
            : this(id, name, topic, description)
        {
            _questions = questions;
        }
    }
}
