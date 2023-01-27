using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GradManSystem1.Data.Migrations
{
    public partial class departmentupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Department_DepartmentId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Proffesor_ProffesorId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Proffesor_Department_DepartmentId",
                table: "Proffesor");

            migrationBuilder.DropIndex(
                name: "IX_Proffesor_DepartmentId",
                table: "Proffesor");

            migrationBuilder.DropIndex(
                name: "IX_Courses_DepartmentId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_ProffesorId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "ProffesorId",
                table: "Courses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProffesorId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proffesor_DepartmentId",
                table: "Proffesor",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DepartmentId",
                table: "Courses",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_ProffesorId",
                table: "Courses",
                column: "ProffesorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Department_DepartmentId",
                table: "Courses",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Proffesor_ProffesorId",
                table: "Courses",
                column: "ProffesorId",
                principalTable: "Proffesor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Proffesor_Department_DepartmentId",
                table: "Proffesor",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
