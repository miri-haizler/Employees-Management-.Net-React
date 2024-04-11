using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employee.Data.Migrations
{
    public partial class roleemployeeuniquekey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RolesEmployees_RoleId",
                table: "RolesEmployees");

            migrationBuilder.CreateIndex(
                name: "IX_RolesEmployees_RoleId_EmployeeDetailsId",
                table: "RolesEmployees",
                columns: new[] { "RoleId", "EmployeeDetailsId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RolesEmployees_RoleId_EmployeeDetailsId",
                table: "RolesEmployees");

            migrationBuilder.CreateIndex(
                name: "IX_RolesEmployees_RoleId",
                table: "RolesEmployees",
                column: "RoleId");
        }
    }
}
