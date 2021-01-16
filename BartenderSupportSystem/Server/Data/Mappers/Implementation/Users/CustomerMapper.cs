using BartenderSupportSystem.Server.Data.DbModels.Users;
using BartenderSupportSystem.Server.Data.DTO.Users;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.Users;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.Users
{
    internal sealed class CustomerMapper : ICustomerMapper
    {
        public CustomerDbModel ToDbModel(CustomerDto item)
        {
            if (item.Id == 0)
            {
                return new CustomerDbModel{FirstName = item.FirstName, LastName = item.LastName};
            }
            else
            {
                return new CustomerDbModel{Id = item.Id, FirstName = item.FirstName, LastName = item.LastName};
            }
        }

        public CustomerDto ToDto(CustomerDbModel item)
        {
            return new CustomerDto
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName
            };
        }
    }
}
