using BartenderSupportSystem.Server.Data.DbModels;
using BartenderSupportSystem.Shared.Models;

namespace BartenderSupportSystem.Server.Data.Mappers.Interfaces
{
    internal interface IBartenderMapper : IMapper<BartenderDto, BartenderDbModel>
    {
    }
}
