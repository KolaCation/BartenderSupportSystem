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
    internal sealed class RatingMapper : IRatingMapper
    {
        private readonly IUserRatingMapper _userRatingMapper;

        public RatingMapper()
        {
            _userRatingMapper = new UserRatingMapper();
        }

        public RatingDbModel ToDbModel(RatingDto item)
        {
            CustomValidator.ValidateObject(item);
            if (item.Id == 0)
            {
                return new RatingDbModel(_userRatingMapper.ToDbModelList(item.RatingList), item.TestId);
            }
            else
            {
                return new RatingDbModel(_userRatingMapper.ToDbModelList(item.RatingList), item.Id, item.TestId);
            }
            
        }

        public RatingDto ToDto(RatingDbModel item)
        {
            CustomValidator.ValidateObject(item);
            return new RatingDto()
            {
                Id = item.Id,
                TestId = item.TestId,
                RatingList = _userRatingMapper.ToDtoList(item.UserRatings)
            };
        }
    }
}