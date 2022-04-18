using System.Collections.Generic;

namespace BartenderSupportSystem.Server.Data.DTO.TestSystem
{
    public sealed class CustomQuestionDto
    {
        public int Id { get; set; }
        public string Statement { get; set; }
        public int TestId { get; set; }
        public List<CustomAnswerDto> Answers { get; set; }
    }
}