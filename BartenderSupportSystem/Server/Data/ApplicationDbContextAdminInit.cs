using BartenderSupportSystem.Server.Data.DbModels;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BartenderSupportSystem.Server.Data.DbModels.Users;
using Microsoft.EntityFrameworkCore;

namespace BartenderSupportSystem.Server.Data
{
    public static class ApplicationDbContextAdminInit
    {
        public static async Task Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            var email = configuration.GetValue<string>("AdminUserEmail");
            var password = configuration.GetValue<string>("AdminUserPswd");

            var user = new ApplicationUser
            {
                Email = email.ToLower(),
                UserName = email.ToLower(),
                EmailConfirmed = true,
                RegistrationDate = DateTimeOffset.Now
            };

            var userExists = await context.Users.AnyAsync(e => e.NormalizedUserName == email.ToUpper());
            if (!userExists)
            {
                await userManager.CreateAsync(user, password);
                await userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Role, "Admin"));
                try
                {
                    await context.CustomersSet.AddAsync(
                        new CustomerDbModel {FirstName = "Husk", LastName = "Heidegger"});
                    await context.SaveChangesAsync();
                    var storedData = context.CustomersSet.OrderByDescending(e => e.Id).FirstOrDefault();
                    user.CustomerId = storedData?.Id ?? default;
                    await context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    user.CustomerId = default;
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}