using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace uPromis.Microservice.ContractAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalID = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AccountField",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    AccountInfoID = table.Column<int>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountField", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AccountField_Accounts_AccountInfoID",
                        column: x => x.AccountInfoID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountTeamComposition",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalID = table.Column<Guid>(nullable: false),
                    TeamMember = table.Column<Guid>(nullable: false),
                    AccountInfoMemberType = table.Column<int>(nullable: false),
                    AccountInfoID = table.Column<int>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTeamComposition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AccountTeamComposition_Accounts_AccountInfoID",
                        column: x => x.AccountInfoID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ContractType = table.Column<int>(maxLength: 50, nullable: false),
                    ContractStatus = table.Column<int>(maxLength: 50, nullable: false),
                    Startdate = table.Column<DateTime>(nullable: false),
                    Enddate = table.Column<DateTime>(nullable: false),
                    Budget = table.Column<double>(nullable: false),
                    ExternalID = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    AccountInfoID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contracts_Accounts_AccountInfoID",
                        column: x => x.AccountInfoID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Proposals",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalID = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Startdate = table.Column<DateTime>(nullable: false),
                    Enddate = table.Column<DateTime>(nullable: false),
                    ProposalStatus = table.Column<int>(maxLength: 50, nullable: false),
                    ProposalType = table.Column<int>(maxLength: 50, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    AccountInfoID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proposals", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Proposals_Accounts_AccountInfoID",
                        column: x => x.AccountInfoID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalID = table.Column<Guid>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Startdate = table.Column<DateTime>(nullable: false),
                    Enddate = table.Column<DateTime>(nullable: false),
                    RequestStatus = table.Column<int>(maxLength: 50, nullable: false),
                    RequestType = table.Column<int>(maxLength: 50, nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true),
                    AccountInfoID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Requests_Accounts_AccountInfoID",
                        column: x => x.AccountInfoID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractPaymentInfo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalID = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PlannedInvoiceDate = table.Column<DateTime>(nullable: false),
                    ActualInvoiceDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    ContractID = table.Column<int>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractPaymentInfo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContractPaymentInfo_Contracts_ContractID",
                        column: x => x.ContractID,
                        principalTable: "Contracts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractTeamComposition",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalID = table.Column<Guid>(nullable: false),
                    TeamMember = table.Column<Guid>(nullable: false),
                    ContractMemberType = table.Column<int>(nullable: false),
                    ContractID = table.Column<int>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTeamComposition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ContractTeamComposition_Contracts_ContractID",
                        column: x => x.ContractID,
                        principalTable: "Contracts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProposalPaymentInfo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalID = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    PlannedInvoiceDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    ProposalID = table.Column<int>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposalPaymentInfo", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProposalPaymentInfo_Proposals_ProposalID",
                        column: x => x.ProposalID,
                        principalTable: "Proposals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProposalTeamComposition",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalID = table.Column<Guid>(nullable: false),
                    TeamMember = table.Column<Guid>(nullable: false),
                    ProposalMemberType = table.Column<int>(nullable: false),
                    ProposalID = table.Column<int>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposalTeamComposition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProposalTeamComposition_Proposals_ProposalID",
                        column: x => x.ProposalID,
                        principalTable: "Proposals",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestTeamComposition",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalID = table.Column<Guid>(nullable: false),
                    TeamMember = table.Column<Guid>(nullable: false),
                    RequestMemberType = table.Column<int>(nullable: false),
                    RequestID = table.Column<int>(nullable: true),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    UpdatedOn = table.Column<DateTime>(nullable: false),
                    UpdatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestTeamComposition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RequestTeamComposition_Requests_RequestID",
                        column: x => x.RequestID,
                        principalTable: "Requests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountField_AccountInfoID",
                table: "AccountField",
                column: "AccountInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountTeamComposition_AccountInfoID",
                table: "AccountTeamComposition",
                column: "AccountInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_ContractPaymentInfo_ContractID",
                table: "ContractPaymentInfo",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_AccountInfoID",
                table: "Contracts",
                column: "AccountInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTeamComposition_ContractID",
                table: "ContractTeamComposition",
                column: "ContractID");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalPaymentInfo_ProposalID",
                table: "ProposalPaymentInfo",
                column: "ProposalID");

            migrationBuilder.CreateIndex(
                name: "IX_Proposals_AccountInfoID",
                table: "Proposals",
                column: "AccountInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_ProposalTeamComposition_ProposalID",
                table: "ProposalTeamComposition",
                column: "ProposalID");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_AccountInfoID",
                table: "Requests",
                column: "AccountInfoID");

            migrationBuilder.CreateIndex(
                name: "IX_RequestTeamComposition_RequestID",
                table: "RequestTeamComposition",
                column: "RequestID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountField");

            migrationBuilder.DropTable(
                name: "AccountTeamComposition");

            migrationBuilder.DropTable(
                name: "ContractPaymentInfo");

            migrationBuilder.DropTable(
                name: "ContractTeamComposition");

            migrationBuilder.DropTable(
                name: "ProposalPaymentInfo");

            migrationBuilder.DropTable(
                name: "ProposalTeamComposition");

            migrationBuilder.DropTable(
                name: "RequestTeamComposition");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Proposals");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
