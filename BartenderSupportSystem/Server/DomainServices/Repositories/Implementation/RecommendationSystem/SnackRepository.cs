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
    public sealed class SnackRepository : ISnackRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ISnackMapper _snackMapper;

        public SnackRepository(ApplicationDbContext context)
        {
            _context = context;
            _snackMapper = new SnackMapper();
        }

        public async Task<IReadOnlyCollection<Snack>> GetAll()
        {
            var snacksList = await _context.SnacksSet.ToListAsync();
            return (from s in snacksList select _snackMapper.DbToDomain(s)).ToList().AsReadOnly();
        }

        public async Task<Snack> GetOne(Guid id)
        {
            var exists = await Exists(id);
            if (exists)
            {
                var snackDbModel = await _context.SnacksSet.FindAsync(id);
                return _snackMapper.DbToDomain(snackDbModel);
            }
            else
            {
                return null;
            }
        }

        public async Task<Snack> AddOne(Snack item)
        {
            var exists = await Exists(item.Id);
            if (!exists)
            {
                await _context.SnacksSet.AddAsync(_snackMapper.DomainToDb(item));
                await _context.SaveChangesAsync();
                return item;
            }
            else
            {
                return null;
            }
        }

        public async Task<Snack> UpdateOne(Snack item)
        {
            var exists = await Exists(item.Id);
            if (exists)
            {
                await Detach(item.Id);
                var enState = _context.SnacksSet.Update(_snackMapper.DomainToDb(item));
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
                var snackDbModel = await _context.SnacksSet.FindAsync(id);
                _context.SnacksSet.Remove(snackDbModel);
                await _context.SaveChangesAsync();
            }
        }

        public Task<bool> Exists(Guid id)
        {
            return _context.SnacksSet.AnyAsync(snack => snack.Id.Equals(id));
        }

        public async Task Detach(Guid id)
        {
            var entity = await _context.SnacksSet.FindAsync(id);
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
