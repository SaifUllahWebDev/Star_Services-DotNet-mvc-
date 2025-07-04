using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarSecurityServices.Migrations
{
    /// <inheritdoc />
    public partial class employeeemail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeEmail",
                table: "RecruitmentApplications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeEmail",
                table: "GuardBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeEmail",
                table: "ElectronicServiceRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeEmail",
                table: "CashServiceBookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeEmail",
                table: "RecruitmentApplications");

            migrationBuilder.DropColumn(
                name: "EmployeeEmail",
                table: "GuardBookings");

            migrationBuilder.DropColumn(
                name: "EmployeeEmail",
                table: "ElectronicServiceRequests");

            migrationBuilder.DropColumn(
                name: "EmployeeEmail",
                table: "CashServiceBookings");
        }
    }
}
