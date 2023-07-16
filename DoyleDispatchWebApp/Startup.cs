using DoyleDispatchWebApp.Data;
using DoyleDispatchWebApp.Helpers;
using DoyleDispatchWebApp.Models;
using DoyleDispatchWebApp.Repositories;
using DoyleDispatchWebApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoyleDispatchWebApp
{
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
            services.AddControllersWithViews();
            services.AddScoped<IPackage, PackageRepo>();
            services.AddScoped<IPhoto, PhotoService>();
            services.AddScoped<IDashboard, DashboardRepo>();
            services.AddScoped<IClient, ClientRepo>();
            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));

            string conString = Configuration["ConnectionStrings:DoyleDispatchConnection"];
            services.AddDbContext<DataContext>(options => options.UseSqlServer(conString));
            services.AddIdentity<Client, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>();
            services.AddMemoryCache();
            services.AddSession();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();           

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });            
            SeedData.SeedUsersAndRolesAsync(app);
            //SeedData.Initialiser(app);
        }
    }
}
