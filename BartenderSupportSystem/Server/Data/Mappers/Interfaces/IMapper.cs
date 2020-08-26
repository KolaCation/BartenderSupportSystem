using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Server.Data.Mappers.Interfaces
{
    internal interface IMapper<TDto, TDbModel>
    {
        TDbModel ToDbModel(TDto item);
        TDto ToDto(TDbModel item);
    }
}
