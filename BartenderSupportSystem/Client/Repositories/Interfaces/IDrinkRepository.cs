using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BartenderSupportSystem.Shared.Models.RecommendationSystem;

namespace BartenderSupportSystem.Client.Repositories.Interfaces
{
    public interface IDrinkRepository
    {
        Task<List<DrinkDto>> GetDrinks();
        Task CreateDrink(DrinkDto drinkDto);
        Task<DrinkDto> GetDrink(Guid id);
        Task UpdateDrink(Guid id, DrinkDto drinkDto);
        Task DeleteDrink(Guid id);
    }
}
