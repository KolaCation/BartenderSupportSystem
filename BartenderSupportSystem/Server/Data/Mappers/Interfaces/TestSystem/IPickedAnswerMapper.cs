using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Shared.Models.TestSystem;
using System.Collections.Generic;

namespace BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem
{
    internal interface IPickedAnswerMapper : IMapper<PickedAnswerDto, PickedAnswerDbModel>
    {
        List<PickedAnswerDbModel> ToDbModelList(List<PickedAnswerDto> items);
        List<PickedAnswerDto> ToDtoList(List<PickedAnswerDbModel> items);
    }
}
