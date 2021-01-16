using System.Collections.Generic;
using System.Linq;
using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.DTO.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.TestSystem
{
    internal sealed class UserRatingMapper : IUserRatingMapper
    {
        public UserRatingDbModel ToDbModel(UserRatingDto item)
        {
            if (item.Id == 0)
            {
                return new UserRatingDbModel
                    {RatingId = item.RatingId, TestId = item.TestId, UserName = item.UserName, Mark = item.Mark};
            }
            else
            {
                return new UserRatingDbModel
                {
                    Id = item.Id, RatingId = item.RatingId, TestId = item.TestId, UserName = item.UserName,
                    Mark = item.Mark
                };
            }
        }

        public UserRatingDto ToDto(UserRatingDbModel item)
        {
            return new UserRatingDto
            {
                Id = item.Id,
                Mark = item.Mark,
                RatingId = item.RatingId,
                TestId = item.TestId,
                UserName = item.UserName
            };
        }

        public List<UserRatingDbModel> ToDbModelList(List<UserRatingDto> items)
        {
            if (items.Count == 0)
            {
                return new List<UserRatingDbModel>();
            }
            else
            {
                return items.Select(ToDbModel).ToList();
            }
        }

        public List<UserRatingDto> ToDtoList(List<UserRatingDbModel> items)
        {
            if (items.Count == 0)
            {
                return new List<UserRatingDto>();
            }
            else
            {
                return items.Select(ToDto).ToList();
            }
        }
    }
}