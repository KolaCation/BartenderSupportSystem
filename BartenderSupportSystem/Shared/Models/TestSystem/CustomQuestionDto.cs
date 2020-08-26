using System;
using System.Collections.Generic;

namespace BartenderSupportSystem.Shared.Models.TestSystem
{
    public sealed class CustomQuestionDto
    {
        public int Id { get; set; }
        public string Statement { get; set; }
        public int TestId { get; set; }
        public CustomTestDto Test { get; set; }
        public List<CustomAnswerDto> Answers { get; set; }
    }
}