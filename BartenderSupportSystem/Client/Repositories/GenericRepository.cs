using BartenderSupportSystem.Client.Helpers;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Client.Repositories
{
    public class GenericRepository<T> : IRepository<T>
    {
        private readonly IHttpService _httpService;
        private readonly string Url;

        public GenericRepository(IHttpService httpService)
        {
            _httpService = httpService;
            Url = $"api/{typeof(T).Name.ToLower()}s";
        }

        public async Task AddOne(T item)
        {
            var result = new HttpResponseWrapper<object>(null, false, null);
            try
            {
                result = await _httpService.Post(Url, item);
            }
            catch(AccessTokenNotAvailableException)
            {
                Console.WriteLine("ERROR ACCESS TOKEN NOT AVAILABLE EXCEPTION");//fix
            }
            
            if(result.Success)
            {
                return;
            }
            else
            {
                throw new ApplicationException(await result.GetBody());
            }
        }

        public async Task DeleteOne(Guid id)
        {
            var result = await _httpService.Delete($"{Url}/{id}");
            if (result.Success)
            {
                return;
            }
            else
            {
                throw new ApplicationException(await result.GetBody());
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var result = await _httpService.Get<IEnumerable<T>>(Url);
            if (result.Success)
            {
                return result.Response;
            }
            else
            {
                throw new ApplicationException(await result.GetBody());
            }
        }

        public async Task<T> GetOne(Guid id)
        {
            var result = await _httpService.Get<T>($"{Url}/{id}");
            if (result.Success)
            {
                return result.Response;
            }
            else
            {
                throw new ApplicationException(await result.GetBody());
            }
        }

        public async Task UpdateOne(Guid id, T item)
        {
            var result = await _httpService.Put($"{Url}/{id}", item);
            if (result.Success)
            {
                return;
            }
            else
            {
                throw new ApplicationException(await result.GetBody());
            }
        }
    }
}
