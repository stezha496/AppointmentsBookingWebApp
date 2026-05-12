using AppointmentBookingProjectFrontEnd.Services;
using System.Net;

namespace AppointmentBookingProjectFrontEnd;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // Shared cookie container so authentication cookie persists across requests
        CookieContainer cookieContainer = new CookieContainer();

        builder.Services.AddHttpClient("AppointmentBookingAPI", client =>
        {
            client.BaseAddress = new Uri("http://localhost:5183");
        }
        )
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = cookieContainer
            });

        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(15);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        builder.Services.AddSingleton<ApiService>(
            sp => new ApiService(sp.GetRequiredService<IHttpClientFactory>(), cookieContainer)
            );

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }
        app.UseRouting();

        app.UseSession();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Login}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}
