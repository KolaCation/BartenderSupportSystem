using BartenderSupportSystem.Server.Data.DbModels;
using BartenderSupportSystem.Server.Data.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.Data.DbModels.TestSystem;
using BartenderSupportSystem.Server.Models;
using IdentityServer4.EntityFramework.Options;
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
        internal DbSet<BartenderDbModel> BartendersSet { get; set; }
        internal DbSet<MealDbModel> MealsSet { get; set; }
        internal DbSet<CustomTestResultDbModel> TestResultsSet { get; set; }
        internal DbSet<PickedAnswerDbModel> PickedAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomQuestionDbModel>().HasMany(q => q.Answers).WithOne(a => a.Question)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CustomTestDbModel>().HasMany(t => t.Questions).WithOne(q => q.Test)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CustomTestResultDbModel>().HasMany(r => r.PickedAnswers)
                .WithOne(a => a.CustomTestResult).OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}