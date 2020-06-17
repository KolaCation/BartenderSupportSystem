using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Domain.RecommendationSystem;
using BartenderSupportSystem.Server.CustomDbContextFolder;
using BartenderSupportSystem.Server.Mappers.Implementation.RecommendationSystem;
using BartenderSupportSystem.Server.Mappers.Interfaces.RecommendationSystem;
using BartenderSupportSystem.Server.Repositories.Interfaces.RecommendationSystem;
using Microsoft.EntityFrameworkCore;

namespace BartenderSupportSystem.Server.Repositories.Implementation.RecommendationSystem
{
    public sealed class CocktailRepository : ICocktailRepository
    {
        private readonly CustomDbContext _context;
        private readonly ICocktailMapper _cocktailMapper;

        public CocktailRepository(CustomDbContext context)
        {
            _context = context;
            _cocktailMapper = new CocktailMapper();
        }

        public async Task<IReadOnlyCollection<Cocktail>> GetAll()
        {
            var cocktailsList = await _context.CocktailsSet.ToListAsync();
            return (from c in cocktailsList select _cocktailMapper.DbToDomain(c)).ToList().AsReadOnly();
        }

        public async Task<Cocktail> GetOne(Guid id)
        {
            var exists = await Exists(id);
            if (exists)
            {
                var cocktailDbModel = await _context.CocktailsSet.FindAsync(id);
                return _cocktailMapper.DbToDomain(cocktailDbModel);
            }
            else
            {
                return null;
            }
        }

        public async Task<Cocktail> AddOne(Cocktail item)
        {
            var exists = await Exists(item.Id);
            if (!exists)
            {
                await _context.CocktailsSet.AddAsync(_cocktailMapper.DomainToDb(item));
                await _context.SaveChangesAsync();
                return item;
            }
            else
            {
                return null;
            }
        }

        public async Task<Cocktail> UpdateOne(Cocktail item)
        {
            var exists = await Exists(item.Id);
            if (exists)
            {
                await Detach(item.Id);
                var enState = _context.CocktailsSet.Update(_cocktailMapper.DomainToDb(item));
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
                var cocktailDbModel = await _context.CocktailsSet.FindAsync(id);
                _context.CocktailsSet.Remove(cocktailDbModel);
                await _context.SaveChangesAsync();
            }
        }

        public Task<bool> Exists(Guid id)
        {
            return _context.CocktailsSet.AnyAsync(cocktail => cocktail.Id.Equals(id));
        }

        public async Task Detach(Guid id)
        {
            var entity = await _context.CocktailsSet.FindAsync(id);
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
