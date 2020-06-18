using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Domain.RecommendationSystem;
using BartenderSupportSystem.Server.Data;
using BartenderSupportSystem.Server.DomainServices.Mappers.Implementation.RecommendationSystem;
using BartenderSupportSystem.Server.DomainServices.Mappers.Interfaces.RecommendationSystem;
using BartenderSupportSystem.Server.DomainServices.Repositories.Interfaces.RecommendationSystem;
using Microsoft.EntityFrameworkCore;

namespace BartenderSupportSystem.Server.DomainServices.Repositories.Implementation.RecommendationSystem
{
    public sealed class DrinkRepository : IDrinkRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDrinkMapper _drinkMapper;

        public DrinkRepository(ApplicationDbContext context)
        {
            _context = context;
            _drinkMapper = new DrinkMapper();
        }

        public async Task<IReadOnlyCollection<Drink>> GetAll()
        {
            var drinksList = await _context.DrinksSet.ToListAsync();
            return (from d in drinksList select _drinkMapper.DbToDomain(d)).ToList().AsReadOnly();
        }

        public async Task<Drink> GetOne(Guid id)
        {
            var exists = await Exists(id);
            if (exists)
            {
                var drinkDbModel = await _context.DrinksSet.FindAsync(id);
                return _drinkMapper.DbToDomain(drinkDbModel);
            }
            else
            {
                return null;
            }
        }

        public async Task<Drink> AddOne(Drink item)
        {
            var exists = await Exists(item.Id);
            if (!exists)
            {
                await _context.DrinksSet.AddAsync(_drinkMapper.DomainToDb(item));
                await _context.SaveChangesAsync();
                return item;
            }
            else
            {
                return null;
            }
        }

        public async Task<Drink> UpdateOne(Drink item)
        {
            var exists = await Exists(item.Id);
            if (exists)
            {
                await Detach(item.Id);
                var enState = _context.DrinksSet.Update(_drinkMapper.DomainToDb(item));
                enState.State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return item;
            }
            else
            {
                return null;
            }
        }

        public async Task DeleteOne(Guid id)
        {
            var exists = await Exists(id);
            if (exists)
            {
                var drinkDbModel = await _context.DrinksSet.FindAsync(id);
                _context.DrinksSet.Remove(drinkDbModel);
                await _context.SaveChangesAsync();
            }
        }

        public Task<bool> Exists(Guid id)
        {
            return _context.DrinksSet.AnyAsync(drink => drink.Id.Equals(id));
        }

        public async Task Detach(Guid id)
        {
            var entity = await _context.DrinksSet.FindAsync(id);
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
