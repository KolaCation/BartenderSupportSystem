using BartenderSupportSystem.Server.Data.DbModels;
using BartenderSupportSystem.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Server.Data.Mappers.Interfaces
{
    internal interface IBartenderMapper : IMapper<BartenderDto, BartenderDbModel>
    {
    }
}
