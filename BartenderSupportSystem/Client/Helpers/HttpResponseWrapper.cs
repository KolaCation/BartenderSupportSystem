using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Client.Helpers
{
    public class HttpResponseWrapper<T>
    {
        public T Response { get; }
        public bool Success { get; }
        public HttpResponseMessage HttpResponseMessage { get; }

        public HttpResponseWrapper(T response, bool success, HttpResponseMessage httpResponseMessage)
        {
            Response = response;
            Success = success;
            HttpResponseMessage = httpResponseMessage;
        }

        public async Task<string> GetBody() => await HttpResponseMessage.Content.ReadAsStringAsync();
    }
}
