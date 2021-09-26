﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using uPromis.Microservice.ProjectAPI;

namespace uPromis.Microservice.ProjectAPI.Migrations
{
    [DbContext(typeof(ProjectDbContext))]
    [Migration("20210906202302_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("uPromis.Microservice.ProjectAPI.Models.Activity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActivityPlanId")
                        .HasColumnType("int");

                    b.Property<string>("ActivityType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

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

                    b.Property<DateTime?>("EnddateActual")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("EnddatePlanned")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExternalID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("PlanTypeID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartdateActual")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartdatePlanned")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.Property<int?>("WorkpackageID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("PlanTypeID");

                    b.HasIndex("WorkpackageID");

                    b.ToTable("Activitiess");
                });

            modelBuilder.Entity("uPromis.Microservice.ProjectAPI.Models.PlanType", b =>
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

                    b.Property<int>("DefaultPeriod")
                        .HasColumnType("int");

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

                    b.ToTable("PlanTypes");
                });

            modelBuilder.Entity("uPromis.Microservice.ProjectAPI.Models.Programme", b =>
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

                    b.Property<string>("ProgrammeType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Programmes");
                });

            modelBuilder.Entity("uPromis.Microservice.ProjectAPI.Models.Project", b =>
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

                    b.Property<DateTime?>("Enddate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ExternalID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ProgrammeID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Startdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("ProgrammeID");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("uPromis.Microservice.ProjectAPI.Models.Workpackage", b =>
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

                    b.Property<int?>("ProjectID")
                        .HasColumnType("int");

                    b.Property<string>("Stage")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Status")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("ProjectID");

                    b.ToTable("Workpackages");
                });

            modelBuilder.Entity("uPromis.Microservice.ProjectAPI.Models.Activity", b =>
                {
                    b.HasOne("uPromis.Microservice.ProjectAPI.Models.PlanType", "PlanType")
                        .WithMany()
                        .HasForeignKey("PlanTypeID");

                    b.HasOne("uPromis.Microservice.ProjectAPI.Models.Workpackage", null)
                        .WithMany("Activities")
                        .HasForeignKey("WorkpackageID");

                    b.Navigation("PlanType");
                });

            modelBuilder.Entity("uPromis.Microservice.ProjectAPI.Models.Project", b =>
                {
                    b.HasOne("uPromis.Microservice.ProjectAPI.Models.Programme", null)
                        .WithMany("Projects")
                        .HasForeignKey("ProgrammeID");
                });

            modelBuilder.Entity("uPromis.Microservice.ProjectAPI.Models.Workpackage", b =>
                {
                    b.HasOne("uPromis.Microservice.ProjectAPI.Models.Project", null)
                        .WithMany("Workpackages")
                        .HasForeignKey("ProjectID");
                });

            modelBuilder.Entity("uPromis.Microservice.ProjectAPI.Models.Programme", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("uPromis.Microservice.ProjectAPI.Models.Project", b =>
                {
                    b.Navigation("Workpackages");
                });

            modelBuilder.Entity("uPromis.Microservice.ProjectAPI.Models.Workpackage", b =>
                {
                    b.Navigation("Activities");
                });
#pragma warning restore 612, 618
        }
    }
}