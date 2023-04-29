using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SledgePlus.Data.Migrations
{
    public partial class lessonNameDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LessonName",
                table: "Lessons");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LessonName",
                table: "Lessons",
                type: "longtext",
                nullable: false);
        }
    }
}
