using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Domain.RecommendationSystem;
using BartenderSupportSystem.Server.Data;
using BartenderSupportSystem.Server.Mappers.Implementation.RecommendationSystem;
using BartenderSupportSystem.Server.Mappers.Interfaces.RecommendationSystem;
using BartenderSupportSystem.Server.Repositories.Interfaces.RecommendationSystem;
using Microsoft.EntityFrameworkCore;

namespace BartenderSupportSystem.Server.Repositories.Implementation.RecommendationSystem
{
    public sealed class IngredientRepository : IIngredientRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IIngredientMapper _ingredientMapper;

        public IngredientRepository(ApplicationDbContext context)
        {
            _context = context;
            _ingredientMapper = new IngredientMapper();
        }

        public async Task<IReadOnlyCollection<Ingredient>> GetAll()
        {
            var ingredientsList = await _context.IngredientsSet.ToListAsync();
            return (from i in ingredientsList select _ingredientMapper.DbToDomain(i)).ToList().AsReadOnly();
        }

        public async Task<Ingredient> GetOne(Guid id)
        {
            var exists = await Exists(id);
            if (exists)
            {
                var ingredientDbModel = await _context.IngredientsSet.FindAsync(id);
                return _ingredientMapper.DbToDomain(ingredientDbModel);
            }
            else
            {
                return null;
            }
        }

        public async Task<Ingredient> AddOne(Ingredient item)
        {
            var exists = await Exists(item.Id);
            if (!exists)
            {
                await _context.IngredientsSet.AddAsync(_ingredientMapper.DomainToDb(item));
                await _context.SaveChangesAsync();
                return item;
            }
            else
            {
                return null;
            }
        }

        public async Task<Ingredient> UpdateOne(Ingredient item)
        {
            var exists = await Exists(item.Id);
            if (exists)
            {
                await Detach(item.Id);
                var enState = _context.IngredientsSet.Update(_ingredientMapper.DomainToDb(item));
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
                var ingredientDbModel = await _context.IngredientsSet.FindAsync(id);
                _context.IngredientsSet.Remove(ingredientDbModel);
                await _context.SaveChangesAsync();
            }
        }

        public Task<bool> Exists(Guid id)
        {
            return _context.IngredientsSet.AnyAsync(ingredient => ingredient.Id.Equals(id));
        }

        public async Task Detach(Guid id)
        {
            var entity = await _context.IngredientsSet.FindAsync(id);
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
