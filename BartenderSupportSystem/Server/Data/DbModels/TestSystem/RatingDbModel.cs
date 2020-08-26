using System;

namespace BartenderSupportSystem.Server.Data.DbModels.TestSystem
{
    internal sealed class RatingDbModel
    {
        public int Id { get; private set; }
        public int TestId { get; private set; }
        public double Mark { get; private set; }
        public int QuantityOfRaters { get; private set; }

        public RatingDbModel(int id, int testId, double mark, int quantityOfRaters)
        {
            Id = id;
            TestId = testId;
            Mark = mark;
            QuantityOfRaters = quantityOfRaters;
        }
    }
}
