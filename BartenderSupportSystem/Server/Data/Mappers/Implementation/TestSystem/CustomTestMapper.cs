using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem;
using BartenderSupportSystem.Shared.Models.TestSystem;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.TestSystem
{
    internal sealed class CustomTestMapper : ICustomTestMapper
    {
        private readonly ApplicationDbContext _context;

        public CustomTestMapper(ApplicationDbContext context)
        {
            _context = context;
        }

        public CustomTestDbModel ToDbModel(CustomTestDto item)
        {
            throw new NotImplementedException();
        }

        public CustomTestDto ToDto(CustomTestDbModel item)
        {
            throw new NotImplementedException();
        }
    }
}
