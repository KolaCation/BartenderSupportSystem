using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem;
using BartenderSupportSystem.Shared.Models.TestSystem;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.TestSystem
{
    internal sealed class CustomAnswerMapper : ICustomAnswerMapper
    {
        public CustomAnswerDbModel ToDbModel(CustomAnswerDto item)
        {
            throw new NotImplementedException();
        }

        public CustomAnswerDto ToDto(CustomAnswerDbModel item)
        {
            throw new NotImplementedException();
        }
    }
}
