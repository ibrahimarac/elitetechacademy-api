﻿// <auto-generated />
using System;
using Elitetech.Academy.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Elitetech.Academy.Data.Migrations
{
    [DbContext(typeof(EliteContext))]
    [Migration("20240224105310_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.27")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Elitetech.Academy.Domain.Entities.Announcement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedTime")
                        .HasColumnOrder(32);

                    b.Property<string>("CreatedUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CreatedUser")
                        .HasColumnOrder(30);

                    b.Property<string>("Detail")
                        .IsRequired()
                        .HasColumnType("nvarchar(4000)")
                        .HasColumnName("Detail")
                        .HasColumnOrder(3);

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("EndDate")
                        .HasColumnOrder(7);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("SendNotification")
                        .HasColumnType("bit")
                        .HasColumnName("SendNotification")
                        .HasColumnOrder(4);

                    b.Property<bool>("SendSms")
                        .HasColumnType("bit")
                        .HasColumnName("SendSms")
                        .HasColumnOrder(5);

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("StartDate")
                        .HasColumnOrder(6);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)")
                        .HasColumnName("Title")
                        .HasColumnOrder(2);

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("UpdatedTime")
                        .HasColumnOrder(33);

                    b.Property<string>("UpdatedUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("UpdatedUser")
                        .HasColumnOrder(31);

                    b.HasKey("Id");

                    b.ToTable("Announcements", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
