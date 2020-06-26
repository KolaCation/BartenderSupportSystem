using System;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Shared.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BartenderSupportSystem.Server.Helpers
{
    public static class HttpContextExtensions
    {
        public static async Task InsertPaginationParamsIntoResponse<T>(this HttpContext httpContext, IQueryable<T> queryable, PaginationDto paginationDto)
        {
            CustomValidator.ValidateObject(httpContext);
            double amountOfObjects = await queryable.CountAsync();
            double totalAmountOfPages = Math.Ceiling(amountOfObjects / paginationDto.AmountOfRecordsPerPage);
            httpContext.Response.Headers.Add("totalAmountOfPages", totalAmountOfPages.ToString());
        }
    }
}
