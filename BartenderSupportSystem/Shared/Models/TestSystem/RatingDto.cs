using System;

namespace BartenderSupportSystem.Shared.Models.TestSystem
{
    public sealed class RatingDto
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public CustomTestDto Test { get; set; }
        public double Mark { get; set; }
        public int QuantityOfRaters { get; set; }
    }
}
