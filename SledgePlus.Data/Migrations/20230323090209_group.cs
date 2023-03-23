using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SledgePlus.Data.Migrations
{
    public partial class group : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4")
                .OldAnnotation("MySQL:Charset", "utf8_unicode_ci");

            migrationBuilder.AlterTable(
                name: "Users")
                .Annotation("MySQL:Charset", "utf8mb4")
                .OldAnnotation("MySQL:Charset", "utf8_unicode_ci");

            migrationBuilder.AlterTable(
                name: "Sections")
                .Annotation("MySQL:Charset", "utf8mb4")
                .OldAnnotation("MySQL:Charset", "utf8_unicode_ci");

            migrationBuilder.AlterTable(
                name: "Roles")
                .Annotation("MySQL:Charset", "utf8mb4")
                .OldAnnotation("MySQL:Charset", "utf8_unicode_ci");

            migrationBuilder.AlterTable(
                name: "Lessons")
                .Annotation("MySQL:Charset", "utf8mb4")
                .OldAnnotation("MySQL:Charset", "utf8_unicode_ci");

            migrationBuilder.AlterTable(
                name: "Group")
                .Annotation("MySQL:Charset", "utf8mb4")
                .OldAnnotation("MySQL:Charset", "utf8_unicode_ci");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8_unicode_ci")
                .OldAnnotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "Users")
                .Annotation("MySQL:Charset", "utf8_unicode_ci")
                .OldAnnotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "Sections")
                .Annotation("MySQL:Charset", "utf8_unicode_ci")
                .OldAnnotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "Roles")
                .Annotation("MySQL:Charset", "utf8_unicode_ci")
                .OldAnnotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "Lessons")
                .Annotation("MySQL:Charset", "utf8_unicode_ci")
                .OldAnnotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.AlterTable(
                name: "Group")
                .Annotation("MySQL:Charset", "utf8_unicode_ci")
                .OldAnnotation("MySQL:Charset", "utf8mb4");
        }
    }
}
