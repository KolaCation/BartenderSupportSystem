namespace BartenderSupportSystem.Server.Data.DTO.TestSystem
{
    public sealed class PickedAnswerDto
    {
        public int Id { get; set; }
        public int CustomTestResultId { get; set; }
        public int CustomAnswerId { get; set; }
        public bool IsPicked { get; set; }
    }
}
