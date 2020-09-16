using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationBackendFinal.Migrations
{
    public partial class addrelationteachercategorytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Teachers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_CategoryId",
                table: "Teachers",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_Categories_CategoryId",
                table: "Teachers",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_Categories_CategoryId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_CategoryId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Teachers");
        }
    }
}
