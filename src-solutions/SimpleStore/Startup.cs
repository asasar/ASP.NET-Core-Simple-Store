using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SimpleStore.Migrations;
using SimpleStore.Models;
using SimpleStore.Services;
 
namespace SimpleStore
{
    [ExcludeFromCodeCoverageAttribute]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SimpleStoreContext>(builder =>
                {
                    string useSqLite = Configuration["Data:useSqLite"] != null? Configuration["Data:useSqLite"].ToLower() : null ;
                    string connection = Configuration.GetConnectionString("SimpleAppConnection");
                    if (useSqLite != "true")
                        builder.UseSqlServer(connection);
                    else
                        builder.UseSqlite(connection);
                });

            services.AddIdentity<SimpleStoreUser, IdentityRole>() 
                .AddEntityFrameworkStores<SimpleStoreContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                // If the LoginPath isn't set, ASP.NET Core defaults 
                // the path to /Account/Login.
                options.LoginPath = "/Auth/Login";
                // If the AccessDeniedPath isn't set, ASP.NET Core defaults 
                // the path to /Account/AccessDenied.
                options.AccessDeniedPath = "/Auth/AccessDenied";
                options.SlidingExpiration = true;
            });


            services.AddLogging();

            services.AddSession();

            services.AddScoped<ISimpleStoreRepository, SimpleStoreRepository>();

            services.AddTransient<SeedData>();
            services.AddScoped<IMailService, MailService>();
            services.AddMvc();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env,  ILoggerFactory loggerFactory, SeedData seedData )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddDebug(LogLevel.Information);
            }
            else
            {
                loggerFactory.AddDebug(LogLevel.Error);
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseSession();

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", Action = "Index" }
                    );
            });

            seedData.EnsureData().Wait();
        }
    }
}
