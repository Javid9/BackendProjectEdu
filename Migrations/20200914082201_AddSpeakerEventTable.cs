using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EducationBackendFinal.Migrations
{
    public partial class AddSpeakerEventTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "UpComingEvents",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Testimonials",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Testimonials",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Testimonials",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Speakers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Image = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpeakerEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UpComingEventId = table.Column<int>(nullable: false),
                    SpeakerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpeakerEvents_Speakers_SpeakerId",
                        column: x => x.SpeakerId,
                        principalTable: "Speakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpeakerEvents_UpComingEvents_UpComingEventId",
                        column: x => x.UpComingEventId,
                        principalTable: "UpComingEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpeakerEvents_SpeakerId",
                table: "SpeakerEvents",
                column: "SpeakerId");

            migrationBuilder.CreateIndex(
                name: "IX_SpeakerEvents_UpComingEventId",
                table: "SpeakerEvents",
                column: "UpComingEventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpeakerEvents");

            migrationBuilder.DropTable(
                name: "Speakers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "UpComingEvents");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Testimonials",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Testimonials",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Testimonials",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
