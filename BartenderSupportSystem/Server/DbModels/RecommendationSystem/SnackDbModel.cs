using System;

namespace BartenderSupportSystem.Server.DbModels.RecommendationSystem
{
    internal sealed class SnackDbModel
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public double PricePerGr { get; private set; }

        public SnackDbModel(Guid id, string name, double pricePerGr)
        {
            Id = id;
            Name = name;
            PricePerGr = pricePerGr;
        }
    }
}