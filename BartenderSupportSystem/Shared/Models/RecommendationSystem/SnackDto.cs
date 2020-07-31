using System;

namespace BartenderSupportSystem.Shared.Models.RecommendationSystem
{
    public sealed class SnackDto : Food
    {
        public SnackDto(Guid id, string name, double pricePerGr) : base(id, name, pricePerGr)
        {
        }
    }
}
