using System;

namespace BartenderSupportSystem.Server.DbModels.RecommendationSystem
{
    internal sealed class ProductDbModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public double PricePerGr { get; private set; }

        public ProductDbModel(Guid id, string name, double pricePerGr)
        {
            Id = id;
            Name = name;
            PricePerGr = pricePerGr;
        }
    }
}
