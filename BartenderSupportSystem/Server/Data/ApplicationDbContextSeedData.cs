using BartenderSupportSystem.Server.Data.DbModels;
using BartenderSupportSystem.Server.Models;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Server.Data
{
    public class ApplicationDbContextSeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            using (var scope = serviceProvider.CreateScope())
            {
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

                if (!context.Users.Any(u => u.NormalizedUserName == email.ToUpper()))
                {

                    var res = await userManager.CreateAsync(user, password);
                    if (res.Succeeded)
                    {
                        await userManager.AddClaimAsync(user, new Claim(JwtClaimTypes.Role, "Admin"));
                        try
                        {
                            context.BartendersSet.Add(new BartenderDbModel("Husk", "Heidegger", null));
                            await context.SaveChangesAsync();
                            var storedData = context.BartendersSet.OrderByDescending(e => e.Id).FirstOrDefault();
                            user.BartenderId = storedData == null ? default : storedData.Id;
                            await context.SaveChangesAsync();
                        }
                        catch (Exception)
                        {
                            user.BartenderId = default;
                            await context.SaveChangesAsync();
                        }
                    }
                }
            }
        }
    }
}
