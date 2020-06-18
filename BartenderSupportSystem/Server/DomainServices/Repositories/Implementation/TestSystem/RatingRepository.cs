using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Domain.TestSystem;
using BartenderSupportSystem.Server.Data;
using BartenderSupportSystem.Server.DomainServices.Mappers.Interfaces.TestSystem;
using BartenderSupportSystem.Server.DomainServices.Repositories.Interfaces.TestSystem;
using BartenderSupportSystem.Server.Mappers.Implementation.TestSystem;
using Microsoft.EntityFrameworkCore;

namespace BartenderSupportSystem.Server.DomainServices.Repositories.Implementation.TestSystem
{
    public sealed class RatingRepository : IRatingRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IRatingMapper _ratingMapper;

        public RatingRepository(ApplicationDbContext context)
        {
            _context = context;
            _ratingMapper = new RatingMapper();
        }

        public async Task<IReadOnlyCollection<Rating>> GetAll()
        {
            var ratingsList = await _context.RatingsSet.ToListAsync();
            return (from r in ratingsList select _ratingMapper.DbToDomain(r)).ToList().AsReadOnly();
        }

        public async Task<Rating> GetOne(Guid id)
        {
            var exists = await Exists(id);
            if (exists)
            {
                var ratingDbModel = await _context.RatingsSet.FindAsync(id);
                return _ratingMapper.DbToDomain(ratingDbModel);
            }
            else
            {
                return null;
            }
        }

        public async Task<Rating> AddOne(Rating item)
        {
            var exists = await Exists(item.Id);
            if (!exists)
            {
                await _context.RatingsSet.AddAsync(_ratingMapper.DomainToDb(item));
                await _context.SaveChangesAsync();
                return item;
            }
            else
            {
                return null;
            }
        }

        public async Task<Rating> UpdateOne(Rating item)
        {
            var exists = await Exists(item.Id);
            if (exists)
            {
                await Detach(item.Id);
                var enState = _context.RatingsSet.Update(_ratingMapper.DomainToDb(item));
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
                var ratingDbModel = await _context.RatingsSet.FindAsync(id);
                _context.RatingsSet.Remove(ratingDbModel);
                await _context.SaveChangesAsync();
            }
        }

        public Task<bool> Exists(Guid id)
        {
            return _context.RatingsSet.AnyAsync(rating => rating.Id.Equals(id));
        }

        public async Task Detach(Guid id)
        {
            var entity = await _context.RatingsSet.FindAsync(id);
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
