﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using uPromis.Microservice.Notification.Data;

namespace uPromis.Microservice.Notification.Migrations
{
    [DbContext(typeof(NotificationDbContext))]
    [Migration("20210424145606_Add expected action")]
    partial class Addexpectedaction
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("uPromis.Microservice.Notification.Model.NotificationEntry", b =>
                {
                    b.Property<string>("NotificationType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SubscriptionID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("URL")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Duedate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Enddate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExpectedAction")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("ID")
                        .HasColumnType("int");

                    b.Property<string>("Salutation")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime?>("Startdate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.HasKey("NotificationType", "SubscriptionID", "URL");

                    b.ToTable("NotificationEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
