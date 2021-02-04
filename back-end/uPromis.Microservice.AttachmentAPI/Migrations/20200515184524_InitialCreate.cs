using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace uPromis.Microservice.AttachmentAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "files",
                columns: table => new
                {
                    id = table.Column<Guid>(maxLength: 40, nullable: false),
                    filename = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    size = table.Column<long>(type: "bigint", nullable: false),
                    blobcontainer = table.Column<string>(unicode: false, nullable: false),
                    blobname = table.Column<string>(unicode: false, nullable: false),
                    uploadedby = table.Column<string>(unicode: false, maxLength: 256, nullable: false),
                    parentitem = table.Column<Guid>(maxLength: 40, nullable: false),
                    uploadedon = table.Column<DateTimeOffset>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_files", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "files");
        }
    }
}
