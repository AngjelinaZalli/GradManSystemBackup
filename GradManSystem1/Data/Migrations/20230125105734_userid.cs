using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GradManSystem1.Data.Migrations
{
    public partial class userid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proffesor_Department_DepartmentId",
                table: "Proffesor");

            migrationBuilder.DropIndex(
                name: "IX_Proffesor_DepartmentId",
                table: "Proffesor");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Proffesor",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Proffesor",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Proffesor_DepartmentId",
                table: "Proffesor",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proffesor_Department_DepartmentId",
                table: "Proffesor",
                column: "DepartmentId",
                principalTable: "Department",
                principalColumn: "Id");
        }
    }
}
