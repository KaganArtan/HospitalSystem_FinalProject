using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalSystem_FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintForAppointments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId_AppointmentDateTime",
                table: "Appointments",
                columns: new[] { "DoctorId", "AppointmentDateTime" },
                unique: true);
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorId_AppointmentDateTime",
                table: "Appointments");
        }
    }
}
