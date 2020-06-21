using System;
using System.Collections.Generic;
using System.Text;

namespace BartenderSupportSystem.Domain.RecommendationSystem
{
    public abstract class Food
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public double PricePerGr { get; private set; }

        protected Food(Guid id, string name, double pricePerGr)
        {
            Id = id;
            Name = name;
            PricePerGr = pricePerGr;
        }
    }
}
