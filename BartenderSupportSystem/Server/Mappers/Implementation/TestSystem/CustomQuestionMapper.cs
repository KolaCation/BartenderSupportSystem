using System.Linq;
using BartenderSupportSystem.Domain.TestSystem;
using BartenderSupportSystem.Server.Data;
using BartenderSupportSystem.Server.DbModels.TestSystem;
using BartenderSupportSystem.Server.Mappers.Interfaces.TestSystem;

namespace BartenderSupportSystem.Server.Mappers.Implementation.TestSystem
{
    internal sealed class CustomQuestionMapper : ICustomQuestionMapper
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomAnswerMapper _customAnswerMapper;

        public CustomQuestionMapper(ApplicationDbContext context)
        {
            _context = context;
            _customAnswerMapper = new CustomAnswerMapper();
        }

        public CustomQuestion DbToDomain(CustomQuestionDbModel dbModel)
        {
            var customQuestionDbModel = _context.QuestionsSet.Find(dbModel.Id);
            _context.Entry(customQuestionDbModel).Collection(item=>item.Answers).Load();
            return new CustomQuestion(dbModel.Id, dbModel.Statement, dbModel.TestId, (from item in customQuestionDbModel.Answers select _customAnswerMapper.DbToDomain(item)).ToList());
        }

        public CustomQuestionDbModel DomainToDb(CustomQuestion domain)
        {
            return new CustomQuestionDbModel(domain.Id, domain.Statement, domain.TestId);
        }
    }
}