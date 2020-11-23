using System.Collections.Generic;
using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Shared.Models.TestSystem;

namespace BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem
{
    internal interface ICustomAnswerMapper: IMapper<CustomAnswerDto, CustomAnswerDbModel>
    {
        List<CustomAnswerDbModel> ToDbModelList(List<CustomAnswerDto> items);
        List<CustomAnswerDto> ToDtoList(List<CustomAnswerDbModel> items);
    }
}
