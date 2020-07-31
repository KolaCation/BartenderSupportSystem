using System;

namespace BartenderSupportSystem.Shared.Models.RecommendationSystem
{
    public sealed class ProductDto : Food, IComponent
    {
        public ProductDto(Guid id, string name, double pricePerGr) : base(id, name, pricePerGr)
        {
        }
    }
}
