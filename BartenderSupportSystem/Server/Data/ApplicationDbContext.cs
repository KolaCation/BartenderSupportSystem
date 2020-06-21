using BartenderSupportSystem.Server.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Server.DomainServices.DbModels;
using BartenderSupportSystem.Server.DomainServices.DbModels.RecommendationSystem;
using BartenderSupportSystem.Server.DomainServices.DbModels.TestSystem;

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
            modelBuilder.Entity<CustomQuestionDbModel>().HasMany(q => q.Answers).WithOne(a => a.Question)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CustomTestDbModel>().HasMany(t => t.Questions).WithOne(q => q.Test)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}
