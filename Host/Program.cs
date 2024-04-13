using AccountManagement.Infrastructure.Configuration;
using BlogManagement.Infrastructure.Configuration;
using CommentManagement.Infrastructure.Configuration;
using Common.Application;
using Common.Application.ZarinPal;
using DiscountManagement.Infrastructure.Configuration;
using Host;
using InventoryManagement.Infrastructure.Configuration;
using InventoryManagement.Presentation.API;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ShopManagement.Infrastructure.Configuration;
using ShopManagement.Presentation.API;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DecorativeStore");
builder.Services.AddHttpContextAccessor();

ShopManagementBootstrapper.Configure(builder.Services, connectionString);
BlogManagementBootstarpper.Configure(builder.Services, connectionString);
CommentManagementBootstrapper.Configure(builder.Services, connectionString);
AccountManagementBootstrapper.Configure(builder.Services, connectionString);
DiscountManagementBootstrapper.Configure(builder.Services, connectionString);
InventoryManagementBootstrapper.Configure(builder.Services, connectionString);

builder.Services.AddTransient<IAuthHelper, AuthHelper>();
builder.Services.AddTransient<IFileUploader, FileUploader>();
builder.Services.AddTransient<IZarinPalFactory, ZarinPalFactory>();

builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Arabic));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

builder.Services.Configure<CookieTempDataProviderOptions>(options => options.Cookie.IsEssential = true);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
{
    o.LoginPath = new PathString("/Account");
    o.LogoutPath = new PathString("/Account");
    o.AccessDeniedPath = new PathString("/AccessDenied");
});

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminArea", builder => builder.RequireRole(new List<string> { "1", "3" }))
    .AddPolicy("Shop", builder => builder.RequireRole(new List<string> { "1" }))
    .AddPolicy("Discount", builder => builder.RequireRole(new List<string> { "1" }))
    .AddPolicy("Account", builder => builder.RequireRole(new List<string> { "1" }));

builder.Services.AddRazorPages().AddMvcOptions(options => options.Filters.Add<SecurityPageFilter>()).AddRazorPagesOptions(options =>
{
    options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
    options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "Shop");
    options.Conventions.AuthorizeAreaFolder("Administration", "/Discounts", "Discount");
    options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");
}).AddApplicationPart(typeof(ProductController).Assembly).AddApplicationPart(typeof(InventoryController).Assembly).AddNewtonsoftJson();

builder.Services.AddSession();

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
