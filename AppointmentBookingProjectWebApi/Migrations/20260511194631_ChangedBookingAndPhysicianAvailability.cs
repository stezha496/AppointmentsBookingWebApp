using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentBookingProjectWebApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangedBookingAndPhysicianAvailability : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Physicians");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "PhysicianAvailabilities",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookedTimeDuration",
                table: "Bookings",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BookedTimeStart",
                table: "Bookings",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "PhysicianAvailabilities");

            migrationBuilder.DropColumn(
                name: "BookedTimeDuration",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "BookedTimeStart",
                table: "Bookings");

            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Physicians",
                type: "INTEGER",
                nullable: true);
        }
    }
}
