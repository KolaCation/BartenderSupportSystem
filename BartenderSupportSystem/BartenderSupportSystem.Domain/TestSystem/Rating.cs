using System;
using System.Collections.Generic;
using System.Text;

namespace BartenderSupportSystem.Domain.TestSystem
{
    public sealed class Rating
    {
        public Guid Id { get; }
        public Guid TestId { get; }
        public double Mark { get; }
        public int QuantityOfRaters { get; }

        public Rating(Guid id, Guid testId, double mark, int quantityOfRaters)
        {
            Id = id;
            TestId = testId;
            Mark = mark;
            QuantityOfRaters = quantityOfRaters;
        }
    }
}
