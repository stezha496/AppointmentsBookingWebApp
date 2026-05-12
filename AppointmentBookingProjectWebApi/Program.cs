
using AppointmentBookingProjectWebApi.Models;
using AppointmentBookingProjectWebApi.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace AppointmentBookingProjectWebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        // .AddJsonOptions is important since it stops circular reference error.
        // When JSON tries to serialize Booking, it includes Physician,
        // which includes its list of Bookings, which includes Physician again and it loops forever.
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        }); ;
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        //builder.Services.AddOpenApi();

        //Register the DbContext (SQLite)
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        // Add the subfolder to the path
        string appFolder = System.IO.Path.Join(path, "AppointmentsApp");
        // Create the folder if it doesn't already exist
        Directory.CreateDirectory(appFolder);
        string DbPath = System.IO.Path.Join(appFolder, "appointments_app.db");

        //Console.WriteLine($"folder: {folder.ToString()} path: {path.ToString()} dbpath: {DbPath}");

        builder.Services.AddDbContext<AppDbContext>(
            options => options.UseSqlite($"Data Source={DbPath}")
            );

        // Add Identity
        builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        // Configure Cookie Authentication
        //builder.Services.ConfigureApplicationCookie(options =>
        //{
        //    // Where to redirect if not logged in
        //    options.LoginPath = "/api/account/login";

        //    // Where to redirect if logged in but wrong role
        //    options.AccessDeniedPath = "/api/account/access-denied";

        //    // How long the cookie lasts
        //    options.ExpireTimeSpan = TimeSpan.FromHours(1);

        //    // Cookie name in the browser
        //    options.Cookie.Name = "AppointmentAppCookie";

        //    // Refresh expiry on activity
        //    options.SlidingExpiration = true;

        //    options.Cookie.SecurePolicy = CookieSecurePolicy.None; // None for HTTP localhost

        //    options.Cookie.SameSite = SameSiteMode.Lax; // Add this for localhost
        //});

        // Add Repositories
        builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
        builder.Services.AddScoped<IBookingRepository, BookingRepository>();
        builder.Services.AddScoped<IPatientRepository, PatientRepository>();
        builder.Services.AddScoped<IPhysicianRepository, PhysicianRepository>();
        builder.Services.AddScoped<IPhysicianAvailabilityRepository, PhysicianAvailabilityRepository>();
        builder.Services.AddScoped<IPatientDetailsRepository, PatientDetailsRepository>();


        // Swashbuckle (swagger)
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        
        // Seed Data
        SeedData.SeedDatabase(app);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            //app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
