using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Client.Helpers
{
    public interface IHttpService
    {
        public Task<HttpResponseWrapper<T>> Get<T>(string url);
        public Task<HttpResponseWrapper<object>> Post<T>(string url, T content);
        public Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T content);
        public  Task<HttpResponseWrapper<object>> Put<T>(string url, T content);
        public Task<HttpResponseWrapper<object>> Delete(string url);
    }
}
