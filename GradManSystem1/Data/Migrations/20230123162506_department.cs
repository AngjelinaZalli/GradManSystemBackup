using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GradManSystem1.Data.Migrations
{
    public partial class department : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfSubjects",
                table: "Department",
                newName: "NumberOfCourses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberOfCourses",
                table: "Department",
                newName: "NumberOfSubjects");
        }
    }
}
