using System;
using System.Collections.Generic;
using System.Text;

namespace BartenderSupportSystem.Domain.TestSystem
{
    public sealed class Rating
    {
        public Guid Id { get; private set; }
        public Guid TestId { get; private set; }
        public double Mark { get; private set; }
        public int QuantityOfRaters { get; private set; }

        public Rating(Guid id, Guid testId, double mark, int quantityOfRaters)
        {
            Id = id;
            TestId = testId;
            Mark = mark;
            QuantityOfRaters = quantityOfRaters;
        }
    }
}
