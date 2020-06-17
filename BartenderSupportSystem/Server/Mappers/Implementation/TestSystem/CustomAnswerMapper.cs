using BartenderSupportSystem.Domain.TestSystem;
using BartenderSupportSystem.Server.DbModels.TestSystem;
using BartenderSupportSystem.Server.Mappers.Interfaces.TestSystem;

namespace BartenderSupportSystem.Server.Mappers.Implementation.TestSystem
{
    internal sealed class CustomAnswerMapper : ICustomAnswerMapper
    {
        public CustomAnswer DbToDomain(CustomAnswerDbModel dbModel)
        {
            return new CustomAnswer(dbModel.Id, dbModel.Statement, dbModel.IsCorrect, dbModel.QuestionId);
        }

        public CustomAnswerDbModel DomainToDb(CustomAnswer domain)
        {
            return new CustomAnswerDbModel(domain.Id, domain.Statement, domain.IsCorrect, domain.QuestionId);
        }
    }
}
