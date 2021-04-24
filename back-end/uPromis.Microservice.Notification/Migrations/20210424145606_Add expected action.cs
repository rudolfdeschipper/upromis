using Microsoft.EntityFrameworkCore.Migrations;

namespace uPromis.Microservice.Notification.Migrations
{
    public partial class Addexpectedaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExpectedAction",
                table: "NotificationEntries",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectedAction",
                table: "NotificationEntries");
        }
    }
}
