using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;

namespace BartenderSupportSystem.Client.Repositories.Interfaces
{
    public interface IBrandRepository
    {
        Task<List<BrandDto>> GetBrands();
        Task CreateBrand(BrandDto brandDto);
        Task<BrandDto> GetBrand(Guid id);
        Task UpdateBrand(Guid id, BrandDto brandDto);
        Task DeleteBrand(Guid id);
    }
}
