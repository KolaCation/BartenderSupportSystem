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
    public sealed class CustomTestRepository : ICustomTestRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomTestMapper _customTestMapper;

        public CustomTestRepository(ApplicationDbContext context)
        {
            _context = context;
            _customTestMapper = new CustomTestMapper(context);
        }

        public async Task<IReadOnlyCollection<CustomTest>> GetAll()
        {
            var testsList = await _context.TestsSet.ToListAsync();
            return (from t in testsList select _customTestMapper.DbToDomain(t)).ToList().AsReadOnly();
        }

        public async Task<CustomTest> GetOne(Guid id)
        {
            var exists = await Exists(id);
            if (exists)
            {
                var customTestDbModel = await _context.TestsSet.FindAsync(id);
                return _customTestMapper.DbToDomain(customTestDbModel);
            }
            else
            {
                return null;
            }
        }

        public async Task<CustomTest> AddOne(CustomTest item)
        {
            var exists = await Exists(item.Id);
            if (!exists)
            {
                await _context.TestsSet.AddAsync(_customTestMapper.DomainToDb(item));
                await _context.SaveChangesAsync();
                return item;
            }
            else
            {
                return null;
            }
        }

        public async Task<CustomTest> UpdateOne(CustomTest item)
        {
            var exists = await Exists(item.Id);
            if (exists)
            {
                await Detach(item.Id);
                var enState = _context.TestsSet.Update(_customTestMapper.DomainToDb(item));
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
                var customTestDbModel = await _context.TestsSet.FindAsync(id);
                _context.TestsSet.Remove(customTestDbModel);
                await _context.SaveChangesAsync();
            }
        }

        public Task<bool> Exists(Guid id)
        {
            return _context.TestsSet.AnyAsync(test => test.Id.Equals(id));
        }

        public async Task Detach(Guid id)
        {
            var entity = await _context.TestsSet.FindAsync(id);
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
