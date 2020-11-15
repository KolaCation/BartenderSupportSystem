using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem;
using BartenderSupportSystem.Shared.Models.TestSystem;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.TestSystem
{
    internal sealed class CustomQuestionMapper : ICustomQuestionMapper
    {
        private readonly ApplicationDbContext _context;

        public CustomQuestionMapper(ApplicationDbContext context)
        {
            _context = context;
        }

        public CustomQuestionDbModel ToDbModel(CustomQuestionDto item)
        {
            throw new NotImplementedException();
        }

        public CustomQuestionDto ToDto(CustomQuestionDbModel item)
        {
            throw new NotImplementedException();
        }
    }
}
