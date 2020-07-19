using System;
using System.Collections.Generic;
using System.Text;

namespace BartenderSupportSystem.Shared.Utils
{
    public sealed class PaginationDto
    {
        public int AmountOfRecordsPerPage { get; set; } = 4;
        public int Page { get; set; } = 1;
    }
}
