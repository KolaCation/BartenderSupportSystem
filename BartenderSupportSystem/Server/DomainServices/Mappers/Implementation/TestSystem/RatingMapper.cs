using BartenderSupportSystem.Domain.TestSystem;
using BartenderSupportSystem.Server.DomainServices.DbModels.TestSystem;
using BartenderSupportSystem.Server.DomainServices.Mappers.Interfaces.TestSystem;

namespace BartenderSupportSystem.Server.Mappers.Implementation.TestSystem
{
    internal sealed class RatingMapper : IRatingMapper
    {
        public Rating DbToDomain(RatingDbModel dbModel)
        {
            return new Rating(dbModel.Id, dbModel.TestId, dbModel.Mark, dbModel.QuantityOfRaters);
        }

        public RatingDbModel DomainToDb(Rating domain)
        {
            return new RatingDbModel(domain.Id, domain.TestId, domain.Mark, domain.QuantityOfRaters);
        }
    }
}
