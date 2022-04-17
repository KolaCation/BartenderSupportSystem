﻿using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.DTO.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.Generic;
using System.Collections.Generic;

namespace BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem
{
    internal interface IPickedAnswerMapper : IMapper<PickedAnswerDto, PickedAnswerDbModel>
    {
        List<PickedAnswerDbModel> ToDbModelList(List<PickedAnswerDto> items);
        List<PickedAnswerDto> ToDtoList(List<PickedAnswerDbModel> items);
    }
}
