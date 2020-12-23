namespace BartenderSupportSystem.Shared.Models.TestSystem
{
    public sealed class CustomAnswerDto
    {
        public int Id { get; set; }
        public string Statement { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}