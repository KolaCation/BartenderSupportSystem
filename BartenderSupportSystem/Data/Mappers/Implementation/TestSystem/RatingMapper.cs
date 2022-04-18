using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.DTO.TestSystem;
using BartenderSupportSystem.Server.Data.Mappers.Interfaces.TestSystem;

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
            if (item.Id == 0)
            {
                return new RatingDbModel
                {
                    UserRatings = _userRatingMapper.ToDbModelList(item.RatingList),
                    TestId = item.TestId
                };
            }
            else
            {
                return new RatingDbModel
                {
                    UserRatings = _userRatingMapper.ToDbModelList(item.RatingList),
                    Id = item.Id,
                    TestId = item.TestId
                };
            }
        }

        public RatingDto ToDto(RatingDbModel item)
        {
            return new RatingDto
            {
                Id = item.Id,
                TestId = item.TestId,
                RatingList = _userRatingMapper.ToDtoList(item.UserRatings)
            };
        }
    }
}