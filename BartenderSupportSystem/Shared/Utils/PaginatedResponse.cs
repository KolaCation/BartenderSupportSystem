using System;
using System.Collections.Generic;
using System.Text;

namespace BartenderSupportSystem.Shared.Utils
{
    public sealed class PaginatedResponse<T>
    {
        public T Content { get; set; }
        public int TotalAmountOfPages { get; set; }
    }
}
