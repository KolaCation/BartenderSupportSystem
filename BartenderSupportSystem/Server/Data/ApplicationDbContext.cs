using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Data.DbModels.Users;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BartenderSupportSystem.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        internal DbSet<BrandDbModel> BrandsSet { get; set; }
        internal DbSet<CocktailDbModel> CocktailsSet { get; set; }
        internal DbSet<DrinkDbModel> DrinksSet { get; set; }
        internal DbSet<IngredientDbModel> IngredientsSet { get; set; }
        internal DbSet<CustomAnswerDbModel> AnswersSet { get; set; }
        internal DbSet<CustomQuestionDbModel> QuestionsSet { get; set; }
        internal DbSet<CustomTestDbModel> TestsSet { get; set; }
        internal DbSet<RatingDbModel> RatingsSet { get; set; }
        internal DbSet<UserRatingDbModel> UserRatingsSet { get; set; }
        internal DbSet<CustomerDbModel> CustomersSet { get; set; }
        internal DbSet<MealDbModel> MealsSet { get; set; }
        internal DbSet<CustomTestResultDbModel> TestResultsSet { get; set; }
        internal DbSet<PickedAnswerDbModel> PickedAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomQuestionDbModel>().HasMany(e => e.Answers).WithOne(e => e.Question)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CustomTestDbModel>().HasMany(e => e.Questions).WithOne(e => e.Test)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CustomTestResultDbModel>().HasMany(e => e.PickedAnswers)
                .WithOne(e => e.CustomTestResult).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<RatingDbModel>().HasMany(e => e.UserRatings).WithOne(e => e.Rating)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}