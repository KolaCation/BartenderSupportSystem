using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Client.Helpers
{
    public sealed class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
        }

        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            var result = await _httpClient.GetAsync(url);
            if (result.IsSuccessStatusCode)
            {
                var response = await Deserialize<T>(result);
                return new HttpResponseWrapper<T>(response, true, result);
            }
            else
            {
                return new HttpResponseWrapper<T>(default, false, result);
            }
        }

        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T content)
        {
            var contentJson = JsonSerializer.Serialize(content, _options);
            var contentString = new StringContent(contentJson, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(url, contentString);
            return new HttpResponseWrapper<object>(null, result.IsSuccessStatusCode, result);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T content)
        {
            var contentJson = JsonSerializer.Serialize(content, _options);
            var contentString = new StringContent(contentJson, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(url, contentString);
            if (result.IsSuccessStatusCode)
            {
                var response = await Deserialize<TResponse>(result);
                return new HttpResponseWrapper<TResponse>(response, true, result);
            }
            else
            {
                return new HttpResponseWrapper<TResponse>(default, false, result);
            }
        }

        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T content)
        {
            var contentJson = JsonSerializer.Serialize(content);
            var contentString = new StringContent(contentJson, Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync(url, contentString);
            return new HttpResponseWrapper<object>(null, result.IsSuccessStatusCode, result);
        }

        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
            var result = await _httpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object>(null, result.IsSuccessStatusCode, result);
        }

        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponseMessage)
        {
            var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<T>(contentStream, _options);
        }
    }
}