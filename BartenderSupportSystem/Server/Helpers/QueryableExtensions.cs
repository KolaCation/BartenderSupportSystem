using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> InsertPagination<T>(this IQueryable<T> queryable, PaginationDto pagination)
        {
            return queryable.Skip((pagination.Page - 1) * pagination.AmountOfRecordsPerPage).Take(pagination.AmountOfRecordsPerPage);
        }
    }
}
