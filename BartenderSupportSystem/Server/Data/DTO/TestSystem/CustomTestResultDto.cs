﻿using System.Collections.Generic;

namespace BartenderSupportSystem.Server.Data.DTO.TestSystem
{
    public sealed class CustomTestResultDto
    {
        public int Id { get; set; }
        public int CustomTestId { get; set; }
        public string UserName { get; set; }
        public List<PickedAnswerDto> PickedAnswers { get; set; }
        public double PersonalMark { get; set; }
    }
}
