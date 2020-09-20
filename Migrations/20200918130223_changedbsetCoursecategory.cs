using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationBackendFinal.Migrations
{
    public partial class changedbsetCoursecategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseCategory_Categories_CategoryId",
                table: "CourseCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseCategory_Courses_CourseId",
                table: "CourseCategory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseCategory",
                table: "CourseCategory");

            migrationBuilder.RenameTable(
                name: "CourseCategory",
                newName: "CourseCategories");

            migrationBuilder.RenameIndex(
                name: "IX_CourseCategory_CourseId",
                table: "CourseCategories",
                newName: "IX_CourseCategories_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseCategory_CategoryId",
                table: "CourseCategories",
                newName: "IX_CourseCategories_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseCategories",
                table: "CourseCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCategories_Categories_CategoryId",
                table: "CourseCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCategories_Courses_CourseId",
                table: "CourseCategories",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseCategories_Categories_CategoryId",
                table: "CourseCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseCategories_Courses_CourseId",
                table: "CourseCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseCategories",
                table: "CourseCategories");

            migrationBuilder.RenameTable(
                name: "CourseCategories",
                newName: "CourseCategory");

            migrationBuilder.RenameIndex(
                name: "IX_CourseCategories_CourseId",
                table: "CourseCategory",
                newName: "IX_CourseCategory_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseCategories_CategoryId",
                table: "CourseCategory",
                newName: "IX_CourseCategory_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseCategory",
                table: "CourseCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCategory_Categories_CategoryId",
                table: "CourseCategory",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseCategory_Courses_CourseId",
                table: "CourseCategory",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
