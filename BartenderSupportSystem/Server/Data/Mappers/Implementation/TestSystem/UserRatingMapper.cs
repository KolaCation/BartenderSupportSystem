using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem;
using BartenderSupportSystem.Shared.Models.TestSystem;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Data.Mappers.Implementation.TestSystem
{
    internal sealed class UserRatingMapper : IUserRatingMapper
    {
        public UserRatingDbModel ToDbModel(UserRatingDto item)
        {
            CustomValidator.ValidateObject(item);
            if (item.Id == 0)
            {
                return new UserRatingDbModel(item.RatingId, item.TestId, item.UserName, item.Mark);
            }
            else
            {
                return new UserRatingDbModel(item.Id, item.RatingId, item.TestId, item.UserName, item.Mark);
            }
        }

        public UserRatingDto ToDto(UserRatingDbModel item)
        {
            CustomValidator.ValidateObject(item);
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
            CustomValidator.ValidateObject(items);
            if (items.Count == 0)
            {
                return new List<UserRatingDbModel>();
            }
            else
            {
                return (from item in items select ToDbModel(item)).ToList();
            }
        }

        public List<UserRatingDto> ToDtoList(List<UserRatingDbModel> items)
        {
            CustomValidator.ValidateObject(items);
            if (items.Count == 0)
            {
                return new List<UserRatingDto>();
            }
            else
            {
                return (from item in items select ToDto(item)).ToList();
            }
        }
    }
}