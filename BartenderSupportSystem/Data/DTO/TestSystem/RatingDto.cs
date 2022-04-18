using System.Collections.Generic;
using System.Linq;

namespace BartenderSupportSystem.Server.Data.DTO.TestSystem
{
    public sealed class RatingDto
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public double Mark => RatingList.Sum(e => e.Mark) / QuantityOfRaters;
        public int QuantityOfRaters => RatingList.Count;
        public List<UserRatingDto> RatingList { get; set; }
    }
}
