using System;
using System.Collections.Generic;
using System.Text;

namespace BartenderSupportSystem.Domain.RecommendationSystem
{
    public sealed class Product : Food
    {
        public Product(Guid id, string name, double pricePerGr) : base(id, name, pricePerGr)
        {
        }
    }
}
