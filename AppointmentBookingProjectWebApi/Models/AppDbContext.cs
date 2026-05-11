using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBookingProjectWebApi.Models;

public class AppDbContext(DbContextOptions<AppDbContext> options): 
    IdentityDbContext<IdentityUser>(options)
{
    public string DbPath { get; set; } = String.Empty;
    #region Database Tables
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Physician> Physicians { get; set; }
    public DbSet<PhysicianAvailability> PhysicianAvailabilities { get; set; }
    #endregion

    #region Identity
    //public DbSet<IdentityUser> AppUsers { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // stores "Pending", "Confirmed", "Cancelled" as string instead of int
        modelBuilder.Entity<Booking>()
            .Property(b => b.Status)
            .HasConversion<string>();
    }

}
