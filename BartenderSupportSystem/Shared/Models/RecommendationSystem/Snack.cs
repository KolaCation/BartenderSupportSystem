using System;

namespace BartenderSupportSystem.Shared.Models.RecommendationSystem
{
    public sealed class Snack : Food
    {
        public Snack(Guid id, string name, double pricePerGr) : base(id, name, pricePerGr)
        {
        }
    }
}
