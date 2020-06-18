using BartenderSupportSystem.Domain.TestSystem;
using BartenderSupportSystem.Server.DomainServices.DbModels.TestSystem;

namespace BartenderSupportSystem.Server.DomainServices.Mappers.Interfaces.TestSystem
{
    internal interface ICustomTestMapper : IMapper<CustomTest, CustomTestDbModel>
    {
    }
}
