using System;

namespace BartenderSupportSystem.Shared.Models.RecommendationSystem
{
    public abstract class Food
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double PricePerGr { get; set; }

        protected Food(Guid id, string name, double pricePerGr)
        {
            Id = id;
            Name = name;
            PricePerGr = pricePerGr;
        }
    }
}
