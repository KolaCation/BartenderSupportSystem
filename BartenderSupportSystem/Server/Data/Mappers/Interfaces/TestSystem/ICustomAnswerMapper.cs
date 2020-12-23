using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Shared.Models.TestSystem;
using System.Collections.Generic;

namespace BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem
{
    internal interface ICustomAnswerMapper : IMapper<CustomAnswerDto, CustomAnswerDbModel>
    {
        List<CustomAnswerDbModel> ToDbModelList(List<CustomAnswerDto> items);
        List<CustomAnswerDto> ToDtoList(List<CustomAnswerDbModel> items);
    }
}
