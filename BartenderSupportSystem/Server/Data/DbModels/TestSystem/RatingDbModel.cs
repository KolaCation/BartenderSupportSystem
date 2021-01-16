using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BartenderSupportSystem.Server.Data.DbModels.TestSystem
{
    internal sealed class RatingDbModel
    {
        [Range(0, int.MaxValue)] public int Id { get; set; }
        [Range(0, int.MaxValue)] public int TestId { get; set; }

        [Required] public List<UserRatingDbModel> UserRatings { get; set; } = new List<UserRatingDbModel>();
    }
}