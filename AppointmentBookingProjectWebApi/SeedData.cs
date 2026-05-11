using AppointmentBookingProjectWebApi.Enums;
using AppointmentBookingProjectWebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingProjectWebApi;

public class SeedData
{
    public static async Task SeedDatabase(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        // Automatically applies any pending migrations and creates the DB if it doesn't exist
        await context.Database.MigrateAsync();

        #region Create Roles
        // Create patient role if it doesn't exist
        if (!await roleManager.RoleExistsAsync("patient"))
        {
            await roleManager.CreateAsync(new IdentityRole("patient"));
        }

        // Create physician role if it doesn't exist
        if (!await roleManager.RoleExistsAsync("physician"))
        {
            await roleManager.CreateAsync(new IdentityRole("physician"));
        }
        #endregion

        // Create in both User and Patient tables
        #region Create Patient Users
        // Seed Patient 1
        if (await userManager.FindByNameAsync("TestP1") == null)
        {
            IdentityUser pUser1 = new IdentityUser
            {
                UserName = "TestP1",
                PhoneNumber = "111-111-1111",
                Email = "test1@gmail.com",
            };

            IdentityResult result = await userManager.CreateAsync(pUser1, "Testpa1!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(pUser1, "patient");
                context.Patients.Add(new Patient
                {
                    Name = "Test Patient 1",
                    Age = 30,
                    Gender = "Female",
                    Height = 150.0,
                    Weight = 132.2,
                    PhoneNumber = pUser1.PhoneNumber,
                    Username = pUser1.UserName,
                    Email = pUser1.Email,
                });
                await context.SaveChangesAsync();
            }
        }

        // Seed Patient 2
        if (await userManager.FindByNameAsync("TestP2") == null)
        {
            IdentityUser pUser2 = new IdentityUser
            {
                UserName = "TestP2",
                PhoneNumber = "222-222-2222",
                Email = "test2@gmail.com",
            };

            IdentityResult result = await userManager.CreateAsync(pUser2, "Testpa2!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(pUser2, "patient");
                context.Patients.Add(new Patient
                {
                    Name = "Test Patient 2",
                    Age = 22,
                    Height = 180.2,
                    Weight = 170.0,
                    Gender = "Male",
                    PhoneNumber = pUser2.PhoneNumber,
                    Username = pUser2.UserName,
                    Email = pUser2.Email,
                });
                await context.SaveChangesAsync();
            }
        }
        #endregion

        // Create in both User and Physician tables
        #region Create Physician Users

        // Seed Physician 1
        if (await userManager.FindByNameAsync("TestPhy1") == null)
        {
            IdentityUser phyUser1 = new IdentityUser
            {
                UserName = "TestPhy1",
                PhoneNumber = "111-111-0000",
                Email = "test1@outlook.com",
            };

            IdentityResult result = await userManager.CreateAsync(phyUser1, "Testphy1!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(phyUser1, "physician");
                context.Physicians.Add(new Physician
                {
                    Name = "Test Physician 1",
                    PhoneNumber = phyUser1.PhoneNumber,
                    Username = phyUser1.UserName,
                    Email = phyUser1.Email,
                });
                await context.SaveChangesAsync();
            }
        }

        // Seed Physician 2
        if (await userManager.FindByNameAsync("TestPhy2") == null)
        {
            IdentityUser phyUser2 = new IdentityUser
            {
                UserName = "TestPhy2",
                PhoneNumber = "222-222-0000",
                Email = "test2@outlook.com",
            };

            IdentityResult result = await userManager.CreateAsync(phyUser2, "Testphy2!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(phyUser2, "physician");
                context.Physicians.Add(new Physician
                {
                    Name = "Test Physician 2",
                    PhoneNumber = phyUser2.PhoneNumber,
                    Username = phyUser2.UserName,
                    Email = phyUser2.Email,
                });
                await context.SaveChangesAsync();
            }
        }

        #endregion

        #region Create Bookings
        // Seed if table is empty
        if (!await context.Bookings.AnyAsync())
        {
            List<Booking> bookingSeedData = new List<Booking>{
    new Booking
    {
        PatientId = 1,
        PhysicianId = 1,
        Status = BookingStatus.Pending,
        ReasonForVisit = "High fever",
        BookedTimeStart = new DateTime(2026, 1, 12, 9, 0, 0),
        BookedTimeDuration = (int)BookingDuration.ThirtyMinutes,
        Created = new DateTime(2026, 1, 10, 14, 23, 0)
    },

    new Booking
    {
        PatientId = 1,
        PhysicianId = 1,
        Status = BookingStatus.Confirmed,
        ReasonForVisit = "Pregnant",
        BookedTimeStart = new DateTime(2026, 2, 3, 11, 0, 0),
        BookedTimeDuration = (int)BookingDuration.FourtyFiveMinutes,
        Created = new DateTime(2026, 1, 29, 8, 41, 0)
    },

    new Booking
    {
        PatientId = 1,
        PhysicianId = 1,
        Status = BookingStatus.Cancelled,
        ReasonForVisit = "Fungal infection",
        BookedTimeStart = new DateTime(2026, 3, 18, 14, 0, 0),
        BookedTimeDuration = (int)BookingDuration.SixtyMinutes,
        Created = new DateTime(2026, 3, 12, 16, 5, 0)
    },

    new Booking
    {
        PatientId = 2,
        PhysicianId = 2,
        Status = BookingStatus.Pending,
        ReasonForVisit = "Cancer check up",
        BookedTimeStart = new DateTime(2026, 5, 7, 10, 0, 0),
        BookedTimeDuration = (int)BookingDuration.ThirtyMinutes,
        Created = new DateTime(2026, 5, 1, 9, 17, 0)
    },

    new Booking
    {
        PatientId = 2,
        PhysicianId = 2,
        Status = BookingStatus.Pending,
        ReasonForVisit = "Broken arm",
        BookedTimeStart = new DateTime(2026, 7, 22, 13, 0, 0),
        BookedTimeDuration = (int)BookingDuration.FourtyFiveMinutes,
        Created = new DateTime(2026, 7, 15, 11, 52, 0)
    },

    new Booking
    {
        PatientId = 2,
        PhysicianId = 2,
        Status = BookingStatus.Pending,
        ReasonForVisit = "Drug addiction treatment",
        BookedTimeStart = new DateTime(2026, 10, 9, 15, 0, 0),
        BookedTimeDuration = (int)BookingDuration.SixtyMinutes,
        Created = new DateTime(2026, 10, 2, 13, 38, 0)
    },
};

            await context.Bookings.AddRangeAsync(bookingSeedData);
            await context.SaveChangesAsync();

            Console.WriteLine("Bookings Seeded");
        }
        #endregion

        #region Create PhysicianAvailability
        // Seed if table is empty
        if (!await context.PhysicianAvailabilities.AnyAsync())
        {
            List<PhysicianAvailability> availabilitiesSeedData = new List<PhysicianAvailability>{
    new PhysicianAvailability
    {
        PhysicianId = 1,
        IsAvailable = true,
        StartTime = new DateTime(2026, 1, 5, 9, 0, 0),
        EndTime = new DateTime(2026, 1, 5, 10, 0, 0)
    },

    new PhysicianAvailability
    {
        PhysicianId = 1,
        IsAvailable = false,
        StartTime = new DateTime(2026, 2, 14, 13, 0, 0),
        EndTime = new DateTime(2026, 2, 14, 14, 0, 0)
    },

    new PhysicianAvailability
    {
        PhysicianId = 1,
        IsAvailable = true,
        StartTime = new DateTime(2026, 3, 21, 15, 0, 0),
        EndTime = new DateTime(2026, 3, 21, 16, 0, 0)
    },

    new PhysicianAvailability
    {
        PhysicianId = 2,
        IsAvailable = false,
        StartTime = new DateTime(2026, 5, 2, 8, 0, 0),
        EndTime = new DateTime(2026, 5, 2, 9, 0, 0)
    },

    new PhysicianAvailability
    {
        PhysicianId = 2,
        IsAvailable = false,
        StartTime = new DateTime(2026, 7, 18, 11, 0, 0),
        EndTime = new DateTime(2026, 7, 18, 12, 0, 0)
    },

    new PhysicianAvailability
    {
        PhysicianId = 2,
        IsAvailable = true,
        StartTime = new DateTime(2026, 9, 30, 14, 0, 0),
        EndTime = new DateTime(2026, 9, 30, 15, 0, 0)
    },

    // Matching booking times from above mock data (not available)

    new PhysicianAvailability
    {
        PhysicianId = 1,
        IsAvailable = false,
        StartTime = new DateTime(2026, 1, 12, 9, 0, 0),
        EndTime = new DateTime(2026, 1, 12, 10, 0, 0)
    },

    new PhysicianAvailability
    {
        PhysicianId = 1,
        IsAvailable = false,
        StartTime = new DateTime(2026, 2, 3, 11, 0, 0),
        EndTime = new DateTime(2026, 2, 3, 12, 0, 0)
    },

    new PhysicianAvailability
    {
        PhysicianId = 1,
        IsAvailable = false,
        StartTime = new DateTime(2026, 3, 18, 14, 0, 0),
        EndTime = new DateTime(2026, 3, 18, 15, 0, 0)
    },

    new PhysicianAvailability
    {
        PhysicianId = 2,
        IsAvailable = false,
        StartTime = new DateTime(2026, 5, 7, 10, 0, 0),
        EndTime = new DateTime(2026, 5, 7, 11, 0, 0)
    },

    new PhysicianAvailability
    {
        PhysicianId = 2,
        IsAvailable = false,
        StartTime = new DateTime(2026, 7, 22, 13, 0, 0),
        EndTime = new DateTime(2026, 7, 22, 14, 0, 0)
    },

    new PhysicianAvailability
    {
        PhysicianId = 2,
        IsAvailable = false,
        StartTime = new DateTime(2026, 10, 9, 15, 0, 0),
        EndTime = new DateTime(2026, 10, 9, 16, 0, 0)
    }
};
            await context.PhysicianAvailabilities.AddRangeAsync(availabilitiesSeedData);
            await context.SaveChangesAsync();

            Console.WriteLine("Physician Availability Seeded");
        }
        #endregion

    }
}