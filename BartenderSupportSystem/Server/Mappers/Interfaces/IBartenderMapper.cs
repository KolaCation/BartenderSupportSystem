using BartenderSupportSystem.Domain;
using BartenderSupportSystem.Server.DbModels;

namespace BartenderSupportSystem.Server.Mappers.Interfaces
{
    internal interface IBartenderMapper : IMapper<Bartender, BartenderDbModel>
    {
    }
}
