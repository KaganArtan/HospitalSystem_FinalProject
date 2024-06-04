using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalSystem_FinalProject.Migrations
{
    /// <inheritdoc />
    public partial class Yenideneme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBooked",
                table: "Appointments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBooked",
                table: "Appointments");
        }
    }
}
