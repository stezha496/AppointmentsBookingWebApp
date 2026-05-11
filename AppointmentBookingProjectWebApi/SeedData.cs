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
                    Age = 11,
                    Gender = "Male",
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
                    Gender = "Female",
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
                    IsAvailable = true,
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
                    IsAvailable = false,
                    PhoneNumber = phyUser2.PhoneNumber,
                    Username = phyUser2.UserName,
                    Email = phyUser2.Email,
                });
                await context.SaveChangesAsync();
            }
        }

        #endregion

        // TODO: Connect to Patient, Physician, PatientDetails
        #region Create Bookings
        // Seed if table is empty
        if (!await context.Bookings.AnyAsync())
        {
            List<Booking> bookingSeedData = new List<Booking>
            {
                new Booking { Status = BookingStatus.Pending, ReasonForVisit = "High fever", Created = DateTime.Now },
                new Booking { Status = BookingStatus.Confirmed, ReasonForVisit = "Pregnant", Created = DateTime.Now },
                new Booking { Status = BookingStatus.Cancelled, ReasonForVisit = "Fungal infection", Created = DateTime.Now },
                new Booking { Status = BookingStatus.Confirmed, ReasonForVisit = "Cancer check up", Created = DateTime.Now },
                new Booking { Status = BookingStatus.Cancelled, ReasonForVisit = "Broken arm", Created = DateTime.Now },
                new Booking { Status = BookingStatus.Pending, ReasonForVisit = "Drug addiction treatment", Created = DateTime.Now },
            };

            //await context.Bookings.AddRangeAsync(bookingSeedData);
            //await context.SaveChangesAsync();

            Console.WriteLine("Bookings Seeded");
        }
        #endregion

        // TODO: Connect to Patient
        #region Create PatientDetails
        // Seed if table is empty
        if (!await context.PatientDetails.AnyAsync())
        {
            List<PatientDetails> detailsSeedData = new List<PatientDetails>
            {
                new PatientDetails { },
                new PatientDetails { },
                new PatientDetails { },
                new PatientDetails { },
                new PatientDetails { },
                new PatientDetails { },
            };

            //await context.PatientDetails.AddRangeAsync(detailsSeedData);
            //await context.SaveChangesAsync();

            Console.WriteLine("Patient Details Seeded");
        }
        #endregion

        // TODO: Connect to Physician
        #region Create PhysicianAvailability
        // Seed if table is empty
        if (!await context.PhysicianAvailabilities.AnyAsync())
        {
            List<PhysicianAvailability> availabilitiesSeedData = new List<PhysicianAvailability>
            {
                new PhysicianAvailability { },
                new PhysicianAvailability { },
                new PhysicianAvailability { },
                new PhysicianAvailability { },
                new PhysicianAvailability { },
                new PhysicianAvailability { },
            };

            //await context.PhysicianAvailabilities.AddRangeAsync(availabilitiesSeedData);
            //await context.SaveChangesAsync();

            Console.WriteLine("Physician Availability Seeded");
        }
        #endregion

        //Console.WriteLine("Seeded");
    }
}