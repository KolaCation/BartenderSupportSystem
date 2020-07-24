using System;

namespace BartenderSupportSystem.Shared.Models.TestSystem
{
    public sealed class CustomAnswer
    {
        public Guid Id { get; set; }
        public string Statement { get; set; }
        public bool IsCorrect { get; set; }
        public Guid QuestionId { get; set; }
        public CustomQuestion Question { get; set; }
    }
}