using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
