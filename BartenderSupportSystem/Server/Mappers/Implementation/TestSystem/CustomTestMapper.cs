using System.Linq;
using BartenderSupportSystem.Domain.TestSystem;
using BartenderSupportSystem.Server.CustomDbContextFolder;
using BartenderSupportSystem.Server.DbModels.TestSystem;
using BartenderSupportSystem.Server.Mappers.Interfaces.TestSystem;

namespace BartenderSupportSystem.Server.Mappers.Implementation.TestSystem
{
    internal sealed class CustomTestMapper : ICustomTestMapper
    {
        private readonly CustomDbContext _context;
        private readonly ICustomQuestionMapper _customQuestionMapper;

        public CustomTestMapper(CustomDbContext context)
        {
            _context = context;
            _customQuestionMapper = new CustomQuestionMapper(context);
        }

        public CustomTest DbToDomain(CustomTestDbModel dbModel)
        {
            var customTestDbModel = _context.TestsSet.Find(dbModel.Id);
            _context.Entry(customTestDbModel).Collection(item => item.Questions).Load();
            return new CustomTest(dbModel.Id, dbModel.Name, dbModel.Topic, dbModel.Description, (from item in customTestDbModel.Questions select _customQuestionMapper.DbToDomain(item)).ToList());
        }

        public CustomTestDbModel DomainToDb(CustomTest domain)
        {
            return new CustomTestDbModel(domain.Id, domain.Name, domain.Topic, domain.Description);
        }
    }
}
