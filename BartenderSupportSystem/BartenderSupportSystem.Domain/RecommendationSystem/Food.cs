using System;
using System.Collections.Generic;
using System.Text;

namespace BartenderSupportSystem.Domain.RecommendationSystem
{
    public abstract class Food
    {
        public Guid Id { get; }
        public string Name { get; }
        public double PricePerGr { get; }

        protected Food(Guid id, string name, double pricePerGr)
        {
            Id = id;
            Name = name;
            PricePerGr = pricePerGr;
        }
    }
}
