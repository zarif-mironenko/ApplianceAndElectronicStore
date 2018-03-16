using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ApplianceAndElectronicStore.Data;
using ApplianceAndElectronicStore.Models;
using ApplianceAndElectronicStore.Services;
using Microsoft.AspNetCore.Http;
using ApplianceAndElectronicStore.Infrastructure;

namespace ApplianceAndElectronicStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddSession();
            services.AddScoped(sp => sp.GetRequiredService<IHttpContextAccessor>().HttpContext);
            services.AddScoped(sp => Cart.GetSessionInstance(sp.GetRequiredService<HttpContext>()));
            services.AddScoped<AppDbContextRepository>();

            services.AddMvc().AddSessionStateTempDataProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider sp)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();

                var dbContext = sp.GetRequiredService<ApplicationDbContext>();
                var userManager = sp.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = sp.GetRequiredService<RoleManager<IdentityRole>>();

                new ApplicationDbInitializer(dbContext, Configuration,
                    userManager, roleManager).SeedData();
            } else {
                app.UseExceptionHandler("/Home/Error");
            } // if

            Server.Initialize(env);

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseSession();

            app.UseMvc(routes => {
                routes.MapRoute(
                   name: "areas",
                   template: "{area:exists}/{controller=Orders}/{action=Index}/{id?}",
                   defaults: new { page = "" }
                );

                routes.MapRoute(
                    name: null,
                    template: "Products/Search/Page{page:int}",
                    defaults: new {
                        controller = "Products",
                        action = "Search"
                    }
                );

                routes.MapRoute(
                     name: "default",
                     template: "{controller=Products}/{action=Index}/{id?}",
                     defaults: new {
                         category = "",
                         page = ""
                     }
                );

                routes.MapRoute(
                    name: null,
                    template: "{area}/{controller}/{action}/Page{page:int}",
                    defaults: new {
                        controller = "Orders",
                        action = "Index"
                    }
                );

                routes.MapRoute(
                    name: null,
                    template: "Products/ProductDetails/Page{page:int}/{id:int}",
                    defaults: new {
                        controller = "Products",
                        action = "ProductDetails",
                        category = ""
                    }
                );

                routes.MapRoute(
                    name: null,
                    template: "Products/ProductDetails/{category}/{id:int}",
                    defaults: new {
                        controller = "Products",
                        action = "ProductDetails",
                        page = ""
                    }
                );

                routes.MapRoute(
                    name: null,
                    template: "Products/ProductDetails/{category}/Page{page:int}/{id:int}",
                    defaults: new { controller = "Products", action = "ProductDetails" }
                );

                routes.MapRoute(
                    name: null,
                    template: "Page{page:int}",
                    defaults: new {
                        controller = "Products",
                        action = "Index",
                        category = ""
                    }
                );
                
                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new {
                        controller = "Products",
                        action = "Index",
                        page = ""
                    }
                );

                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{page:int}",
                    defaults: new { controller = "Products", action = "Index" }
                );
            });
        }
    }
}
