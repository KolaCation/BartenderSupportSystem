using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using System.Collections.Generic;
using BartenderSupportSystem.Server.Data.DTO.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.Generic;

namespace BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem
{
    internal interface ICustomAnswerMapper : IMapper<CustomAnswerDto, CustomAnswerDbModel>
    {
        List<CustomAnswerDbModel> ToDbModelList(List<CustomAnswerDto> items);
        List<CustomAnswerDto> ToDtoList(List<CustomAnswerDbModel> items);
    }
}
