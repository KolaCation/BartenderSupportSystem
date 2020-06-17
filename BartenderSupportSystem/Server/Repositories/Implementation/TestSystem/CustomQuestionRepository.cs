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
    public sealed class CustomQuestionRepository : ICustomQuestionRepository
    {
        private readonly CustomDbContext _context;
        private readonly ICustomQuestionMapper _customQuestionMapper;

        public CustomQuestionRepository(CustomDbContext context)
        {
            _context = context;
            _customQuestionMapper = new CustomQuestionMapper(context);
        }

        public async Task<IReadOnlyCollection<CustomQuestion>> GetAll()
        {
            var questionsList = await _context.QuestionsSet.ToListAsync();
            return (from q in questionsList select _customQuestionMapper.DbToDomain(q)).ToList().AsReadOnly();
        }

        public async Task<CustomQuestion> GetOne(Guid id)
        {
            var exists = await Exists(id);
            if (exists)
            {
                var customQuestionDbModel = await _context.QuestionsSet.FindAsync(id);
                return _customQuestionMapper.DbToDomain(customQuestionDbModel);
            }
            else
            {
                return null;
            }
        }

        public async Task<CustomQuestion> AddOne(CustomQuestion item)
        {
            var exists = await Exists(item.Id);
            if (!exists)
            {
                await _context.QuestionsSet.AddAsync(_customQuestionMapper.DomainToDb(item));
                await _context.SaveChangesAsync();
                return item;
            }
            else
            {
                return null;
            }
        }

        public async Task<CustomQuestion> UpdateOne(CustomQuestion item)
        {
            var exists = await Exists(item.Id);
            if (exists)
            {
                await Detach(item.Id);
                var enState = _context.QuestionsSet.Update(_customQuestionMapper.DomainToDb(item));
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
                var customQuestionDbModel = await _context.QuestionsSet.FindAsync(id);
                _context.QuestionsSet.Remove(customQuestionDbModel);
                await _context.SaveChangesAsync();
            }
        }

        public Task<bool> Exists(Guid id)
        {
            return _context.QuestionsSet.AnyAsync(question => question.Id.Equals(id));
        }

        public async Task Detach(Guid id)
        {
            var entity = await _context.QuestionsSet.FindAsync(id);
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
