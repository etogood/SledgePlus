using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SledgePlus.Data.Migrations
{
    public partial class docs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LessonDocumentName",
                table: "Lessons",
                type: "longtext",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LessonDocumentName",
                table: "Lessons");
        }
    }
}
