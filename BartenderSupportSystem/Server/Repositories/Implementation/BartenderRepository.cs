using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Domain;
using BartenderSupportSystem.Server.Data;
using BartenderSupportSystem.Server.Mappers.Implementation;
using BartenderSupportSystem.Server.Mappers.Interfaces;
using BartenderSupportSystem.Server.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BartenderSupportSystem.Server.Repositories.Implementation
{
    public sealed class BartenderRepository : IBartenderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IBartenderMapper _bartenderMapper;

        public BartenderRepository(ApplicationDbContext context)
        {
            _context = context;
            _bartenderMapper = new BartenderMapper();
        }

        public async Task<IReadOnlyCollection<Bartender>> GetAll()
        {
            var bartendersList = await _context.BartendersSet.ToListAsync();
            return (from b in bartendersList select _bartenderMapper.DbToDomain(b)).ToList().AsReadOnly();
        }

        public async Task<Bartender> GetOne(Guid id)
        {
            var exists = await Exists(id);
            if (exists)
            {
                var bartenderDbModel = await _context.BartendersSet.FindAsync(id);
                return _bartenderMapper.DbToDomain(bartenderDbModel);
            }
            else
            {
                return null;
            }
        }

        public async Task<Bartender> AddOne(Bartender item)
        {
            var exists = await Exists(item.Id);
            if (!exists)
            {
                await _context.BartendersSet.AddAsync(_bartenderMapper.DomainToDb(item));
                await _context.SaveChangesAsync();
                return item;
            }
            else
            {
                return null;
            }
        }

        public async Task<Bartender> UpdateOne(Bartender item)
        {
            var exists = await Exists(item.Id);
            if (exists)
            {
                await Detach(item.Id);
                var enState = _context.BartendersSet.Update(_bartenderMapper.DomainToDb(item));
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
                var bartenderDbModel = await _context.BartendersSet.FindAsync(id);
                _context.BartendersSet.Remove(bartenderDbModel);
                await _context.SaveChangesAsync();
            }
        }

        public Task<bool> Exists(Guid id)
        {
            return _context.BartendersSet.AnyAsync(bartender=>bartender.Id.Equals(id));
        }

        public async Task Detach(Guid id)
        {
            var entity = await _context.BartendersSet.FindAsync(id);
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
