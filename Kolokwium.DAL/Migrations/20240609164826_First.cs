using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kolokwium.DAL.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_departments_DepartmentId",
                table: "employees");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "employees",
                newName: "DepartmentIdFK");

            migrationBuilder.RenameIndex(
                name: "IX_employees_DepartmentId",
                table: "employees",
                newName: "IX_employees_DepartmentIdFK");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_departments_DepartmentIdFK",
                table: "employees",
                column: "DepartmentIdFK",
                principalTable: "departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employees_departments_DepartmentIdFK",
                table: "employees");

            migrationBuilder.RenameColumn(
                name: "DepartmentIdFK",
                table: "employees",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_employees_DepartmentIdFK",
                table: "employees",
                newName: "IX_employees_DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_employees_departments_DepartmentId",
                table: "employees",
                column: "DepartmentId",
                principalTable: "departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
