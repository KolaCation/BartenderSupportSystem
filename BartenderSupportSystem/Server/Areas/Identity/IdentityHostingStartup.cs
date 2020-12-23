using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(BartenderSupportSystem.Server.Areas.Identity.IdentityHostingStartup))]
namespace BartenderSupportSystem.Server.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}