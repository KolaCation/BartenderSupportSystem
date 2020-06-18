using BartenderSupportSystem.Domain;
using BartenderSupportSystem.Server.DomainServices.DbModels;

namespace BartenderSupportSystem.Server.DomainServices.Mappers.Interfaces
{
    internal interface IBartenderMapper : IMapper<Bartender, BartenderDbModel>
    {
    }
}
