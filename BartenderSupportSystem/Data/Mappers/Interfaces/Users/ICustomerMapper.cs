using BartenderSupportSystem.Server.Data.DbModels.Users;
using BartenderSupportSystem.Server.Data.DTO.Users;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.Generic;

namespace BartenderSupportSystem.Server.Data.Mappers.Interfaces.Users
{
    internal interface ICustomerMapper : IMapper<CustomerDto, CustomerDbModel>
    {
    }
}
