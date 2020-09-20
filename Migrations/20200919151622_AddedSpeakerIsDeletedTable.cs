using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationBackendFinal.Migrations
{
    public partial class AddedSpeakerIsDeletedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Speakers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Speakers");
        }
    }
}
