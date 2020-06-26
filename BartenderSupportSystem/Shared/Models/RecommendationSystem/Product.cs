using System;

namespace BartenderSupportSystem.Shared.Models.RecommendationSystem
{
    public sealed class Product : Food, IComponent
    {
        public Product(Guid id, string name, double pricePerGr) : base(id, name, pricePerGr)
        {
        }
    }
}
