using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationBackendFinal.Migrations
{
    public partial class addnewcolmuncoursetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutCourse",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Certification",
                table: "Courses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourseFee",
                table: "Courses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "HowToApply",
                table: "Courses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutCourse",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Certification",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseFee",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "HowToApply",
                table: "Courses");
        }
    }
}
