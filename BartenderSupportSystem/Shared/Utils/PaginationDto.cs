using System;
using System.Collections.Generic;
using System.Text;

namespace BartenderSupportSystem.Shared.Utils
{
    public sealed class PaginationDto
    {
        public int TotalAmountOfPages { get; set; }
        public int AmountOfRecordsPerPage { get; set; } = 4;
        public int CurrentPage { get; set; } = 1;
    }
}
