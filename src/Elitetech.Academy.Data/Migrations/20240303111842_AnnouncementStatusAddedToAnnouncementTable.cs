using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Elitetech.Academy.Data.Migrations
{
    public partial class AnnouncementStatusAddedToAnnouncementTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnnouncementStatus",
                table: "Announcements",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("Relational:ColumnOrder", 7);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnnouncementStatus",
                table: "Announcements");
        }
    }
}
