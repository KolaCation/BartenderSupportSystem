using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Server.Data.DbModels.TestSystem
{
    internal sealed class RatingDbModel
    {
        public int Id { get; private set; }
        public int TestId { get; private set; }
        public List<UserRatingDbModel> UserRatings { get; private set; }
        
        public RatingDbModel(int testId)
        {
            TestId = testId;
            UserRatings = new List<UserRatingDbModel>();
        }

        public RatingDbModel(List<UserRatingDbModel> userRatings, int testId) : this(testId)
        {
            CustomValidator.ValidateObject(userRatings);
            UserRatings = userRatings;
        }

        public RatingDbModel(int id, int testId)
        {
            Id = id;
            TestId = testId;
            UserRatings = new List<UserRatingDbModel>();
        }

        public RatingDbModel(List<UserRatingDbModel> userRatings, int id, int testId) : this(id, testId)
        {
            CustomValidator.ValidateObject(userRatings);
            UserRatings = userRatings;
        }
    }
}