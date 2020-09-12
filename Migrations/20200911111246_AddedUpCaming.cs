using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationBackendFinal.Migrations
{
    public partial class AddedUpCaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "UpComingEvents");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "UpComingEvents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "UpComingEvents");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "UpComingEvents",
                nullable: false,
                defaultValue: 0);
        }
    }
}
