namespace BartenderSupportSystem.Shared.Models.TestSystem
{
    public sealed class UserRatingDto
    {
        public int Id { get; set; }
        public int RatingId { get; set; }
        public int TestId { get; set; }
        public string UserName { get; set; }
        public double Mark { get; set; }

    }
}
