using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.DTO.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.Generic;
using System.Collections.Generic;

namespace BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem
{
    internal interface ICustomAnswerMapper : IMapper<CustomAnswerDto, CustomAnswerDbModel>
    {
        List<CustomAnswerDbModel> ToDbModelList(List<CustomAnswerDto> items);
        List<CustomAnswerDto> ToDtoList(List<CustomAnswerDbModel> items);
    }
}
