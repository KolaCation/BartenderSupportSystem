using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Domain.TestSystem;
using BartenderSupportSystem.Server.CustomDbContextFolder;
using BartenderSupportSystem.Server.Mappers.Implementation.TestSystem;
using BartenderSupportSystem.Server.Mappers.Interfaces.TestSystem;
using BartenderSupportSystem.Server.Repositories.Interfaces.TestSystem;
using Microsoft.EntityFrameworkCore;

namespace BartenderSupportSystem.Server.Repositories.Implementation.TestSystem
{
    public sealed class CustomAnswerRepository : ICustomAnswerRepository
    {
        private readonly CustomDbContext _context;
        private readonly ICustomAnswerMapper _customAnswerMapper;

        public CustomAnswerRepository(CustomDbContext context)
        {
            _context = context;
            _customAnswerMapper = new CustomAnswerMapper();
        }

        public async Task<IReadOnlyCollection<CustomAnswer>> GetAll()
        {
            var answersList = await _context.AnswersSet.ToListAsync();
            return (from a in answersList select _customAnswerMapper.DbToDomain(a)).ToList().AsReadOnly();
        }

        public async Task<CustomAnswer> GetOne(Guid id)
        {
            var exists = await Exists(id);
            if (exists)
            {
                var customAnswerDbModel = await _context.AnswersSet.FindAsync(id);
                return _customAnswerMapper.DbToDomain(customAnswerDbModel);
            }
            else
            {
                return null;
            }
        }

        public async Task<CustomAnswer> AddOne(CustomAnswer item)
        {
            var exists = await Exists(item.Id);
            if (!exists)
            {
                await _context.AnswersSet.AddAsync(_customAnswerMapper.DomainToDb(item));
                await _context.SaveChangesAsync();
                return item;
            }
            else
            {
                return null;
            }
        }

        public async Task<CustomAnswer> UpdateOne(CustomAnswer item)
        {
            var exists = await Exists(item.Id);
            if (exists)
            {
                await Detach(item.Id);
                var enState = _context.AnswersSet.Update(_customAnswerMapper.DomainToDb(item));
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
                var customAnswerDbModel = await _context.AnswersSet.FindAsync(id);
                _context.AnswersSet.Remove(customAnswerDbModel);
                await _context.SaveChangesAsync();
            }
        }

        public Task<bool> Exists(Guid id)
        {
            return _context.AnswersSet.AnyAsync(answer => answer.Id.Equals(id));
        }

        public async Task Detach(Guid id)
        {
            var entity = await _context.AnswersSet.FindAsync(id);
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
