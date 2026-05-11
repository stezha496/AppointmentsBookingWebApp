using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentBookingProjectWebApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPatientDetailsAndPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_PatientDetails_detailsId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "PatientDetails");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_detailsId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "detailsId",
                table: "Bookings");

            migrationBuilder.AddColumn<double>(
                name: "Height",
                table: "Patients",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "Patients",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Patients");

            migrationBuilder.AddColumn<int>(
                name: "detailsId",
                table: "Bookings",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PatientDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientDetails_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_detailsId",
                table: "Bookings",
                column: "detailsId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDetails_PatientId",
                table: "PatientDetails",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_PatientDetails_detailsId",
                table: "Bookings",
                column: "detailsId",
                principalTable: "PatientDetails",
                principalColumn: "Id");
        }
    }
}
