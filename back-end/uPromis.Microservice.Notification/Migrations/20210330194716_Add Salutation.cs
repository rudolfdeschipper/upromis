using Microsoft.EntityFrameworkCore.Migrations;

namespace uPromis.Microservice.Notification.Migrations
{
    public partial class AddSalutation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Salutation",
                table: "NotificationEntries",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salutation",
                table: "NotificationEntries");
        }
    }
}
