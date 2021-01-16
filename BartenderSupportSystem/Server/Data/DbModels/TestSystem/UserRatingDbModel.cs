using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BartenderSupportSystem.Server.Data.DbModels.TestSystem
{
    internal sealed class UserRatingDbModel
    {
        [Range(0, int.MaxValue)] public int Id { get; set; }
        [Range(0, int.MaxValue)] public int RatingId { get; set; }
        [ForeignKey(nameof(RatingId))] public RatingDbModel Rating { get; set; }
        [Range(0, int.MaxValue)] public int TestId { get; set; }
        [Required] public string UserName { get; set; }
        [Range(0, 5)] public double Mark { get; set; }
    }
}