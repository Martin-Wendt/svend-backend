﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SitrepAPI.DbContexts;

#nullable disable
#pragma warning disable

namespace SitrepAPI.Migrations
{
    [DbContext(typeof(SitrepDbContext))]
    [Migration("20220310100351_caseImage_refactor")]
    partial class caseImage_refactor
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("SitrepAPI.Entities.Case", b =>
                {
                    b.Property<int>("CaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CaseId"), 1L, 1);

                    b.Property<string>("AssigneeId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PriorityId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CaseId");

                    b.HasIndex("PriorityId");

                    b.HasIndex("StatusId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("SitrepAPI.Entities.CaseImage", b =>
                {
                    b.Property<int>("CaseImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CaseImageId"), 1L, 1);

                    b.Property<int>("CaseId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CaseImageId");

                    b.HasIndex("CaseId");

                    b.ToTable("CaseImages");
                });

            modelBuilder.Entity("SitrepAPI.Entities.CaseLog", b =>
                {
                    b.Property<int>("CaseLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CaseLogId"), 1L, 1);

                    b.Property<int>("CaseId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedById")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CaseLogId");

                    b.HasIndex("CaseId");

                    b.ToTable("CaseLogs");
                });

            modelBuilder.Entity("SitrepAPI.Entities.Priority", b =>
                {
                    b.Property<int>("PriorityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PriorityId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PriorityId");

                    b.ToTable("Priority");

                    b.HasData(
                        new
                        {
                            PriorityId = 1,
                            Name = "Unset"
                        },
                        new
                        {
                            PriorityId = 2,
                            Name = "Lav"
                        },
                        new
                        {
                            PriorityId = 3,
                            Name = "Mellem"
                        },
                        new
                        {
                            PriorityId = 4,
                            Name = "Høj"
                        });
                });

            modelBuilder.Entity("SitrepAPI.Entities.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusId");

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            StatusId = 1,
                            Name = "Oprettet"
                        },
                        new
                        {
                            StatusId = 2,
                            Name = "Godkendt"
                        },
                        new
                        {
                            StatusId = 3,
                            Name = "Igangværende"
                        },
                        new
                        {
                            StatusId = 4,
                            Name = "Afsluttet"
                        },
                        new
                        {
                            StatusId = 5,
                            Name = "Afvist"
                        },
                        new
                        {
                            StatusId = 6,
                            Name = "Slettet"
                        });
                });

            modelBuilder.Entity("SitrepAPI.Entities.Case", b =>
                {
                    b.HasOne("SitrepAPI.Entities.Priority", "Priority")
                        .WithMany()
                        .HasForeignKey("PriorityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SitrepAPI.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Priority");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("SitrepAPI.Entities.CaseImage", b =>
                {
                    b.HasOne("SitrepAPI.Entities.Case", "Case")
                        .WithMany("Images")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Case");
                });

            modelBuilder.Entity("SitrepAPI.Entities.CaseLog", b =>
                {
                    b.HasOne("SitrepAPI.Entities.Case", null)
                        .WithMany("Logs")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SitrepAPI.Entities.Case", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Logs");
                });
#pragma warning restore 612, 618
        }
    }
#pragma warning restore

}
