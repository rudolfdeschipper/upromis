﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using uPromis.Microservice.ContractAPI;

namespace uPromis.Microservice.ContractAPI.Migrations
{
    [DbContext(typeof(ContractDbContext))]
    partial class ContractDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.AccountField", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountInfoID")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExternalID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.HasIndex("AccountInfoID");

                    b.ToTable("AccountField");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.AccountInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ExternalID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.AccountTeamComposition", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountInfoID")
                        .HasColumnType("int");

                    b.Property<string>("AccountInfoMemberType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExternalID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TeamMember")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("AccountInfoID");

                    b.ToTable("AccountTeamComposition");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.Client", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountInfoID")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ContactMail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ExternalID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MainContact")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("AccountInfoID");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.Contract", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountInfoID")
                        .HasColumnType("int");

                    b.Property<double>("Budget")
                        .HasColumnType("float");

                    b.Property<int?>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ContactMail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ContractStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ContractType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Enddate")
                        .HasColumnType("datetime2");

                    b.Property<double>("ExpenseBudget")
                        .HasColumnType("float");

                    b.Property<Guid>("ExternalID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MainContact")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("ParentContractId")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ProposalId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Startdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.HasIndex("AccountInfoID");

                    b.HasIndex("ClientId");

                    b.HasIndex("ParentContractId");

                    b.HasIndex("ProposalId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.ContractPaymentInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ActualInvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int?>("ContractID")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ExternalID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("PlannedInvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("ContractID");

                    b.ToTable("ContractPaymentInfo");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.ContractTeamComposition", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContractID")
                        .HasColumnType("int");

                    b.Property<string>("ContractMemberType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExternalID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TeamMember")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("ContractID");

                    b.ToTable("ContractTeamComposition");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.Proposal", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountInfoID")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Enddate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExternalID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProposalStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ProposalType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("Startdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("AccountInfoID");

                    b.ToTable("Proposals");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.ProposalPaymentInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ExternalID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("PlannedInvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ProposalID")
                        .HasColumnType("int");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("ProposalID");

                    b.ToTable("ProposalPaymentInfo");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.ProposalTeamComposition", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExternalID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ProposalID")
                        .HasColumnType("int");

                    b.Property<string>("ProposalMemberType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TeamMember")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("ProposalID");

                    b.ToTable("ProposalTeamComposition");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.Request", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountInfoID")
                        .HasColumnType("int");

                    b.Property<DateTime>("AckActualdate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("AckPlandate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ExternalID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OfferActualdate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("OfferPlandate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RequestStatus")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("RequestType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("RequestorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Startdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("YNActualdate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("YNPlandate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("AccountInfoID");

                    b.HasIndex("RequestorId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.RequestTeamComposition", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExternalID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("RequestID")
                        .HasColumnType("int");

                    b.Property<string>("RequestMemberType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TeamMember")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("RequestID");

                    b.ToTable("RequestTeamComposition");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.AccountField", b =>
                {
                    b.HasOne("uPromis.Microservice.ContractAPI.Models.AccountInfo", "AccountInfo")
                        .WithMany("AccountFields")
                        .HasForeignKey("AccountInfoID");

                    b.Navigation("AccountInfo");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.AccountTeamComposition", b =>
                {
                    b.HasOne("uPromis.Microservice.ContractAPI.Models.AccountInfo", "AccountInfo")
                        .WithMany("Teammembers")
                        .HasForeignKey("AccountInfoID");

                    b.Navigation("AccountInfo");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.Client", b =>
                {
                    b.HasOne("uPromis.Microservice.ContractAPI.Models.AccountInfo", null)
                        .WithMany("Clients")
                        .HasForeignKey("AccountInfoID");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.Contract", b =>
                {
                    b.HasOne("uPromis.Microservice.ContractAPI.Models.AccountInfo", null)
                        .WithMany("Contracts")
                        .HasForeignKey("AccountInfoID");

                    b.HasOne("uPromis.Microservice.ContractAPI.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId");

                    b.HasOne("uPromis.Microservice.ContractAPI.Models.Contract", "ParentContract")
                        .WithMany()
                        .HasForeignKey("ParentContractId");

                    b.HasOne("uPromis.Microservice.ContractAPI.Models.Proposal", "Proposal")
                        .WithMany()
                        .HasForeignKey("ProposalId");

                    b.Navigation("Client");

                    b.Navigation("ParentContract");

                    b.Navigation("Proposal");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.ContractPaymentInfo", b =>
                {
                    b.HasOne("uPromis.Microservice.ContractAPI.Models.Contract", "Contract")
                        .WithMany("Payments")
                        .HasForeignKey("ContractID");

                    b.Navigation("Contract");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.ContractTeamComposition", b =>
                {
                    b.HasOne("uPromis.Microservice.ContractAPI.Models.Contract", "Contract")
                        .WithMany("Teammembers")
                        .HasForeignKey("ContractID");

                    b.Navigation("Contract");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.Proposal", b =>
                {
                    b.HasOne("uPromis.Microservice.ContractAPI.Models.AccountInfo", null)
                        .WithMany("Proposals")
                        .HasForeignKey("AccountInfoID");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.ProposalPaymentInfo", b =>
                {
                    b.HasOne("uPromis.Microservice.ContractAPI.Models.Proposal", "Proposal")
                        .WithMany("Payments")
                        .HasForeignKey("ProposalID");

                    b.Navigation("Proposal");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.ProposalTeamComposition", b =>
                {
                    b.HasOne("uPromis.Microservice.ContractAPI.Models.Proposal", "Proposal")
                        .WithMany("Teammembers")
                        .HasForeignKey("ProposalID");

                    b.Navigation("Proposal");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.Request", b =>
                {
                    b.HasOne("uPromis.Microservice.ContractAPI.Models.AccountInfo", null)
                        .WithMany("Requests")
                        .HasForeignKey("AccountInfoID");

                    b.HasOne("uPromis.Microservice.ContractAPI.Models.Client", "Requestor")
                        .WithMany()
                        .HasForeignKey("RequestorId");

                    b.Navigation("Requestor");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.RequestTeamComposition", b =>
                {
                    b.HasOne("uPromis.Microservice.ContractAPI.Models.Request", "Request")
                        .WithMany("Teammembers")
                        .HasForeignKey("RequestID");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.AccountInfo", b =>
                {
                    b.Navigation("AccountFields");

                    b.Navigation("Clients");

                    b.Navigation("Contracts");

                    b.Navigation("Proposals");

                    b.Navigation("Requests");

                    b.Navigation("Teammembers");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.Contract", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("Teammembers");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.Proposal", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("Teammembers");
                });

            modelBuilder.Entity("uPromis.Microservice.ContractAPI.Models.Request", b =>
                {
                    b.Navigation("Teammembers");
                });
#pragma warning restore 612, 618
        }
    }
}
