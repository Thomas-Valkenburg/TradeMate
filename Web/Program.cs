using DAL_Factory;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Web;

public static class Program
{
	internal static readonly Factory.ServiceType ServiceType = Factory.ServiceType.Sqlite;

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddControllersWithViews();

        builder.Services.AddSession();

        builder.Services.AddRequestLocalization(options =>
        {
	        var supportedCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
	        var defaultCulture    = new CultureInfo("en-US");

	        options.SupportedCultures     = supportedCultures;
	        options.DefaultRequestCulture = new RequestCulture(defaultCulture);
        });

        builder.Services.AddAuthentication(options =>
        {
        })
        .AddCookie(options =>
        {
            options.LoginPath = "/Account/Login";
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseRequestLocalization();

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}