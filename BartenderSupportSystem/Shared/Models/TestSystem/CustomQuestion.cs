using System;
using System.Collections.Generic;

namespace BartenderSupportSystem.Shared.Models.TestSystem
{
    public sealed class CustomQuestion
    {
        public Guid Id { get; set; }
        public string Statement { get; set; }
        public Guid TestId { get; set; }
        public CustomTest Test { get; set; }
        public List<CustomAnswer> Answers { get; set; }
    }
}