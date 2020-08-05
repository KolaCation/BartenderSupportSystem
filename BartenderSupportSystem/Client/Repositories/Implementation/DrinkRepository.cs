using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Client.Helpers;
using BartenderSupportSystem.Client.Repositories.Interfaces;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;

namespace BartenderSupportSystem.Client.Repositories.Implementation
{
    public sealed class DrinkRepository : IDrinkRepository
    {
        private readonly IHttpService _httpService;
        private const string Url = "api/drinks";

        public DrinkRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<DrinkDto>> GetDrinks()
        {
            var response = await _httpService.Get<List<DrinkDto>>(Url);
            if (response.Success)
            {
                return response.Response;
            }
            else
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task CreateDrink(DrinkDto drinkDto)
        {
            var response = await _httpService.Post(Url, drinkDto);
            if (!response.Success)
            {
                throw new ArgumentException(await response.GetBody());
            }
        }

        public async Task<DrinkDto> GetDrink(Guid id)
        {
            var response = await _httpService.Get<DrinkDto>($"{Url}/{id}");
            if (response.Success)
            {
                return response.Response;
            }
            else
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task UpdateDrink(Guid id, DrinkDto drinkDto)
        {
            var response = await _httpService.Put($"{Url}/{id}", drinkDto);
            if (!response.Success)
            {
                throw new ArgumentException(await response.GetBody());
            }
        }

        public async Task DeleteDrink(Guid id)
        {
            var response = await _httpService.Delete($"{Url}/{id}");
            if (!response.Success)
            {
                throw new ArgumentException(await response.GetBody());
            }
        }
    }
}
