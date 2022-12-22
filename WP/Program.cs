using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.CodeAnalysis.Host;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System.Globalization;
using System.Net;
using System.Reflection;
using WP.Models;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'WebAppContextConnection' not found.");

builder.Services.AddDbContext<WebAppContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddTransient<WP.Models.ILanguageService, LanguageService>();
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
builder.Services.AddIdentity<IdentityUser,IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false).AddDefaultTokenProviders()
    .AddEntityFrameworkStores<WebAppContext>();
builder.Services.Configure<RequestLocalizationOptions>(
    options =>
    {
        var supportedCultures = new List<CultureInfo>
            {
                            new CultureInfo("en-US"),
                            new CultureInfo("es-ES"),
                            new CultureInfo("ja-JP"),
                            /*new CultureInfo("de-DE"),*/
            };



        options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");

        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
        options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());

    });
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IEmailSender,EmailSender>();
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation() ;
builder.Services.AddMvc()
                .AddViewLocalization()
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {

                        var assemblyName = new AssemblyName(typeof(ShareResource).GetTypeInfo().Assembly.FullName);

                        return factory.Create("ShareResource", assemblyName.Name);

                    };

                });
builder.Services.AddDbContext<WebAppContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
var locOptions = ((IApplicationBuilder)app).ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();

app.UseRequestLocalization(locOptions.Value);
app.UseRouting();
app.UseAuthentication();;
app.MapRazorPages();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
