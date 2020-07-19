using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Client.Helpers
{
    public static class IHttpServiceExtensions
    {
        public static async Task<T> GetHelper<T>(this IHttpService httpService, string url)
        {
            var result = await httpService.Get<T>(url);
            if (result.Success)
            {
                return result.Response;
            }
            else
            {
                throw new ApplicationException(await result.GetBody());
            }
        }

        public static async Task<PaginatedResponse<T>> GetHelper<T>(this IHttpService httpService, string url,
            PaginationDto paginationDto)
        {
            var urlWithQueryString = "";
            if (url.Contains("?"))
            {
                urlWithQueryString =
                    $"{url}&currentPage={paginationDto.CurrentPage}&amountOfRecordsPerPage={paginationDto.AmountOfRecordsPerPage}";
            }
            else
            {
                urlWithQueryString =
                    $"{url}?currentPage={paginationDto.CurrentPage}&amountOfRecordsPerPage={paginationDto.AmountOfRecordsPerPage}";
            }

            var result = await httpService.Get<T>(urlWithQueryString);
            if (result.Success)
            {
                var amountOfPages = int.Parse(result.HttpResponseMessage.Headers.GetValues("totalAmountOfPages")
                    .FirstOrDefault());
                var paginatedResponse = new PaginatedResponse<T>
                {
                    Content = result.Response,
                    TotalAmountOfPages = amountOfPages
                };
                return paginatedResponse;
            }
            else
            {
                throw new ApplicationException(await result.GetBody());
            }
        }
    }
}