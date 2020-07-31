using System;

namespace BartenderSupportSystem.Shared.Models.TestSystem
{
    public sealed class CustomAnswerDto
    {
        public Guid Id { get; set; }
        public string Statement { get; set; }
        public bool IsCorrect { get; set; }
        public Guid QuestionId { get; set; }
        public CustomQuestionDto Question { get; set; }
    }
}