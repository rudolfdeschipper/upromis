using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace uPromis.Microservice.ContractAPI.Migrations
{
    public partial class Addrequestdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Enddate",
                table: "Requests",
                newName: "YNPlandate");

            migrationBuilder.AddColumn<DateTime>(
                name: "AckActualdate",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "AckPlandate",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OfferActualdate",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OfferPlandate",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RequestorId",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "YNActualdate",
                table: "Requests",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Contracts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "ContactMail",
                table: "Contracts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestorId",
                table: "Requests",
                column: "RequestorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Clients_RequestorId",
                table: "Requests",
                column: "RequestorId",
                principalTable: "Clients",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Clients_RequestorId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_RequestorId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "AckActualdate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "AckPlandate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "OfferActualdate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "OfferPlandate",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RequestorId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "YNActualdate",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "YNPlandate",
                table: "Requests",
                newName: "Enddate");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Contracts",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ContactMail",
                table: "Contracts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
