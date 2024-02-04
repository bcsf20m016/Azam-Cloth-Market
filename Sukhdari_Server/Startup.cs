using Business;
using Business.IRepo;
using DataAccess.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sukhdari_Server.Data;
using Sukhdari_Server.Service;
using Sukhdari_Server.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using System.Threading.Tasks;
using Syncfusion.Blazor;
using Microsoft.AspNetCore.HttpOverrides;

namespace Sukhdari_Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>

            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders()
                .AddDefaultUI();
            services.AddScoped<IDbInitializer, DbInitializer>();

            services.AddScoped<IStoreRepo, StoreRepo>();
            services.AddScoped<ICategoryRepo,CategoryRepo>();
            services.AddScoped<IProductRepo,ProductRepo>();
            services.AddScoped<IProductImageRepo, ProductImageRepo>();
            services.AddScoped<IImageUpload, ImageUpload>();
            services.AddScoped<IUserIPRepo, UserIPRepo>();
            services.AddScoped<IStoreImageRepo, StoreImageRepo>();
            services.AddScoped<ITagRepo, TagRepo>();
            services.AddScoped<ITagTypeRepo, TagTypeRepo>();
            services.AddScoped<IStoreTagRepo, StoreTagRepo>();
            services.AddScoped<ICountDetailsRepo, CountDetailsRepo>();

            services.AddHttpContextAccessor();
            services.AddRazorPages();
            services.AddSyncfusionBlazor(/*options => { options.IgnoreScriptIsolation = true; }*/);
            services.AddHttpContextAccessor();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
           // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            dbInitializer.Initalize();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
