﻿using System;
using System.Collections.Generic;

namespace BartenderSupportSystem.Shared.Models.TestSystem
{
    public sealed class CustomTestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }
        public string Description { get; set; }
        public List<CustomQuestionDto> Questions { get; set; }
    }
}