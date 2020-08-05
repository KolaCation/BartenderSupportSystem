using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Client.Helpers;
using BartenderSupportSystem.Client.Repositories.Interfaces;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;

namespace BartenderSupportSystem.Client.Repositories.Implementation
{
    public sealed class BrandRepository : IBrandRepository
    {
        private readonly IHttpService _httpService;
        private const string Url = "api/brands";

        public BrandRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task<List<BrandDto>> GetBrands()
        {
            var response = await _httpService.Get<List<BrandDto>>(Url);
            if (response.Success)
            {
                return response.Response;
            }
            else
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task CreateBrand(BrandDto brandDto)
        {
            var response = await _httpService.Post(Url, brandDto);
            if (!response.Success)
            {
                throw new ArgumentException(await response.GetBody());
            }
        }

        public async Task<BrandDto> GetBrand(Guid id)
        {
            var response = await _httpService.Get<BrandDto>($"{Url}/{id}");
            if (response.Success)
            {
                return response.Response;
            }
            else
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task UpdateBrand(Guid id, BrandDto brandDto)
        {
            var response = await _httpService.Put($"{Url}/{id}", brandDto);
            if (!response.Success)
            {
                throw new ArgumentException(await response.GetBody());
            }
        }

        public async Task DeleteBrand(Guid id)
        {
            var response = await _httpService.Delete($"{Url}/{id}");
            if (!response.Success)
            {
                throw new ArgumentException(await response.GetBody());
            }
        }
    }
}
