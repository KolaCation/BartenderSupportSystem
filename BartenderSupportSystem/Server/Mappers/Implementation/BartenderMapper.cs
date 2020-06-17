using BartenderSupportSystem.Domain;
using BartenderSupportSystem.Server.DbModels;
using BartenderSupportSystem.Server.Mappers.Interfaces;

namespace BartenderSupportSystem.Server.Mappers.Implementation
{
    internal sealed class BartenderMapper : IBartenderMapper
    {
        public Bartender DbToDomain(BartenderDbModel dbModel)
        {
            return new Bartender(dbModel.Id, dbModel.FirstName, dbModel.LastName, dbModel.Experience, dbModel.PhotoPath);
        }

        public BartenderDbModel DomainToDb(Bartender domain)
        {
            return new BartenderDbModel(domain.Id, domain.FirstName, domain.LastName, domain.Experience, domain.PhotoPath);
        }
    }
}
