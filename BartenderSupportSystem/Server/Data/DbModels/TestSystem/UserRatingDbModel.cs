using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BartenderSupportSystem.Server.Data.DbModels.TestSystem
{
    internal sealed class UserRatingDbModel
    {
        public int Id { get; private set; }
        public int RatingId { get; private set; }
        [ForeignKey("RatingId")] public RatingDbModel Rating { get; private set; }
        public int TestId { get; private set; }
        public string UserName { get; private set; }
        public double Mark { get; private set; }

        public UserRatingDbModel(int ratingId, int testId, string userName, double mark)
        {
            RatingId = ratingId;
            TestId = testId;
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Mark = mark;
        }

        public UserRatingDbModel(int id, int ratingId, int testId, string userName, double mark) : this(ratingId,
            testId, userName, mark)
        {
            Id = id;
        }
    }
}