using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Shared.Models.TestSystem;

namespace BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem
{
    internal interface ICustomQuestionMapper : IMapper<CustomQuestionDto, CustomQuestionDbModel>
    {
        List<CustomQuestionDbModel> ToDbModelList(List<CustomQuestionDto> items);
        List<CustomQuestionDto> ToDtoList(List<CustomQuestionDbModel> items);
    }
}
