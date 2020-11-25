using System;
using System.Collections.Generic;
using System.Text;

namespace BartenderSupportSystem.Shared.Models.TestSystem
{
    public sealed class PickedAnswerDto
    {
        public int Id { get; set; }
        public int CustomTestResultId { get; set; }
        public CustomTestResultDto CustomTestResultDto { get; set; }
        public int CustomAnswerId { get; set; }
        public bool IsPicked { get; set; }
    }
}
