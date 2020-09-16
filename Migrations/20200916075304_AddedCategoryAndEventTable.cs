using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationBackendFinal.Migrations
{
    public partial class AddedCategoryAndEventTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "UpComingEvents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UpComingEvents_CategoryId",
                table: "UpComingEvents",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_UpComingEvents_Categories_CategoryId",
                table: "UpComingEvents",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UpComingEvents_Categories_CategoryId",
                table: "UpComingEvents");

            migrationBuilder.DropIndex(
                name: "IX_UpComingEvents_CategoryId",
                table: "UpComingEvents");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "UpComingEvents");
        }
    }
}
