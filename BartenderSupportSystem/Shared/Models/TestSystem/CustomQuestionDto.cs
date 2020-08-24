using System;
using System.Collections.Generic;

namespace BartenderSupportSystem.Shared.Models.TestSystem
{
    public sealed class CustomQuestionDto
    {
        public Guid Id { get; set; }
        public string Statement { get; set; }
        public Guid TestId { get; set; }
        public CustomTestDto Test { get; set; }
        public List<CustomAnswerDto> Answers { get; set; }
    }
}