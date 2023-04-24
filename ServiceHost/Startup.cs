using AccountManagement.Infrastructure.Configuration;
using BlogManagement.Infrastructure.Configuration;
using CommentManagement.Infrastructure.Configuration;
using DiscountManagement.Infrastructure.Configuration;
using Framework.Application;
using Framework.Application.ZarinPal;
using InventoryManagement.Infrastructure.Configuration;
using InventoryManagement.Presentation.API;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopManagement.Infrastructure.Configuration;
using ShopManagement.Infrastructure.Presentation;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace ServiceHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("LampShade");
            services.AddHttpContextAccessor();

            ShopManagementBootstrapper.Configure(services, connectionString);
            BlogManagementBootstarpper.Configure(services, connectionString);
            CommentManagementBootstrapper.Configure(services, connectionString);
            AccountManagementBootstrapper.Configure(services, connectionString);
            DiscountManagementBootstrapper.Configure(services, connectionString);
            InventoryManagementBootstrapper.Configure(services, connectionString);

            services.AddTransient<IAuthHelper, AuthHelper>();
            services.AddTransient<IFileUploader, FileUploader>();
            services.AddTransient<IZarinPalFactory, ZarinPalFactory>();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.Lax;
            });
            services.Configure<CookieTempDataProviderOptions>(options => options.Cookie.IsEssential = true);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
            {
                o.LoginPath = new PathString("/Account");
                o.LogoutPath = new PathString("/Account");
                o.AccessDeniedPath = new PathString("/AccessDenied");
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminArea", builder => builder.RequireRole(new List<string> { "1", "3" }));
                options.AddPolicy("Shop", builder => builder.RequireRole(new List<string> { "1" }));
                options.AddPolicy("Discount", builder => builder.RequireRole(new List<string> { "1" }));
                options.AddPolicy("Account", builder => builder.RequireRole(new List<string> { "1" }));
            });

            services.AddRazorPages().AddMvcOptions(options => options.Filters.Add<SecurityPageFilter>()).AddRazorPagesOptions(options =>
            {
                options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
                options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "Shop");
                options.Conventions.AuthorizeAreaFolder("Administration", "/Discounts", "Discount");
                options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");
            }).AddApplicationPart(typeof(ProductController).Assembly).AddApplicationPart(typeof(InventoryController).Assembly).AddNewtonsoftJson();
            services.AddSession();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}