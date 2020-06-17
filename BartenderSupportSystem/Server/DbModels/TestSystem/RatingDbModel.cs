using System;

namespace BartenderSupportSystem.Server.DbModels.TestSystem
{
    internal sealed class RatingDbModel
    {
        public Guid Id { get; private set; }
        public Guid TestId { get; private set; }
        public double Mark { get; private set; }
        public int QuantityOfRaters { get; private set; }

        public RatingDbModel(Guid id, Guid testId, double mark, int quantityOfRaters)
        {
            Id = id;
            TestId = testId;
            Mark = mark;
            QuantityOfRaters = quantityOfRaters;
        }
    }
}
