using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.DTO.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.Generic;
using System.Collections.Generic;

namespace BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem
{
    internal interface IUserRatingMapper : IMapper<UserRatingDto, UserRatingDbModel>
    {
        List<UserRatingDbModel> ToDbModelList(List<UserRatingDto> items);
        List<UserRatingDto> ToDtoList(List<UserRatingDbModel> items);
    }
}
