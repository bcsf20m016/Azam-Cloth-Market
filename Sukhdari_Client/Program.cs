using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sukhdari_Client.Service;
using Sukhdari_Client.Service.IService;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using CurrieTechnologies.Razor.SweetAlert2;
using Blazored.LocalStorage;

namespace Sukhdari_Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("IP", (options) => {
                options.BaseAddress = new Uri("https://jsonip.com");
            });
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseApiUrl", "https://localhost:44353/")) });
            builder.Services.AddScoped<IStoreService, StoreService>();
            builder.Services.AddScoped<IUserIpService, UserIpService>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddSweetAlert2();
            await builder.Build().RunAsync();
        }
    }
}
