using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.DTO.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.Generic;
using System.Collections.Generic;

namespace BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem
{
    internal interface ICustomQuestionMapper : IMapper<CustomQuestionDto, CustomQuestionDbModel>
    {
        List<CustomQuestionDbModel> ToDbModelList(List<CustomQuestionDto> items);
        List<CustomQuestionDto> ToDtoList(List<CustomQuestionDbModel> items);
    }
}
