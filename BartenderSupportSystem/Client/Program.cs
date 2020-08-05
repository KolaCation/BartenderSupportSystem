using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazor.FileReader;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using BartenderSupportSystem.Client.Helpers;
using BartenderSupportSystem.Client.Repositories;
using BartenderSupportSystem.Client.Repositories.Implementation;
using BartenderSupportSystem.Client.Repositories.Interfaces;

namespace BartenderSupportSystem.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddHttpClient("BartenderSupportSystem.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BartenderSupportSystem.ServerAPI"));
            builder.Services.AddFileReaderService();
            builder.Services.AddScoped<IHttpService, HttpService>();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            builder.Services.AddScoped<IDrinkRepository, DrinkRepository>();
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddApiAuthorization();

            await builder.Build().RunAsync();
        }
    }
}
