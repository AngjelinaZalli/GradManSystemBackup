using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GradManSystem1.Data.Migrations
{
    public partial class departmentid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Courses_CoursesId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Proffesor_ProffesorId",
                table: "Grade");

            migrationBuilder.DropForeignKey(
                name: "FK_Grade_Student_StudentId",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_Grade_CoursesId",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_Grade_ProffesorId",
                table: "Grade");

            migrationBuilder.DropIndex(
                name: "IX_Grade_StudentId",
                table: "Grade");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Grade");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Proffesor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Proffesor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Grade",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Grade_CoursesId",
                table: "Grade",
                column: "CoursesId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_ProffesorId",
                table: "Grade",
                column: "ProffesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_StudentId",
                table: "Grade",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Courses_CoursesId",
                table: "Grade",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Proffesor_ProffesorId",
                table: "Grade",
                column: "ProffesorId",
                principalTable: "Proffesor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grade_Student_StudentId",
                table: "Grade",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
