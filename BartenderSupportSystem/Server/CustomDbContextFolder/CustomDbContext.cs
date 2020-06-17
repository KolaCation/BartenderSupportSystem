using BartenderSupportSystem.Server.DbModels;
using BartenderSupportSystem.Server.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.DbModels.TestSystem;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BartenderSupportSystem.Server.CustomDbContextFolder
{
    public sealed class CustomDbContext : ApiAuthorizationDbContext<IdentityUser>
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {
        }

        internal DbSet<BrandDbModel> BrandsSet { get; set; }
        internal DbSet<CocktailDbModel> CocktailsSet { get; set; }
        internal DbSet<DrinkDbModel> DrinksSet { get; set; }
        internal DbSet<IngredientDbModel> IngredientsSet { get; set; }
        internal DbSet<MenuDbModel> MenusSet { get; set; }
        internal DbSet<ProductDbModel> ProductsSet { get; set; }
        internal DbSet<SnackDbModel> SnacksSet { get; set; }
        internal DbSet<CustomAnswerDbModel> AnswersSet { get; set; }
        internal DbSet<CustomQuestionDbModel> QuestionsSet { get; set; }
        internal DbSet<CustomTestDbModel> TestsSet { get; set; }
        internal DbSet<RatingDbModel> RatingsSet { get; set; }
        internal DbSet<BartenderDbModel> BartendersSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CustomQuestionDbModel>().HasMany(q => q.Answers).WithOne(a => a.Question)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CustomTestDbModel>().HasMany(t => t.Questions).WithOne(q => q.Test)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}