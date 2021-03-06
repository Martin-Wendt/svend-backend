// <auto-generated />
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
    [Migration("20220301101054_relationalchanges")]
    partial class relationalchanges
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

                    b.ToTable("Cases");

                    b.HasData(
                        new
                        {
                            CaseId = -1,
                            CreatedAt = new DateTimeOffset(new DateTime(1, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)),
                            Description = "Description-1",
                            Location = "Location-1",
                            PriorityId = 1,
                            StatusId = 1,
                            Title = "Title-1",
                            UserId = "auth0|614c239f53b183006ace3593"
                        },
                        new
                        {
                            CaseId = -2,
                            CreatedAt = new DateTimeOffset(new DateTime(1, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)),
                            Description = "Description-2",
                            Location = "Location-2",
                            PriorityId = 1,
                            StatusId = 1,
                            Title = "Title-2",
                            UserId = "auth0|614c239f53b183006ace3593"
                        },
                        new
                        {
                            CaseId = -3,
                            CreatedAt = new DateTimeOffset(new DateTime(1, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)),
                            Description = "Description-3",
                            Location = "Location-3",
                            PriorityId = 1,
                            StatusId = 1,
                            Title = "Title-3",
                            UserId = "auth0|614c239f53b183006ace3593"
                        },
                        new
                        {
                            CaseId = -4,
                            CreatedAt = new DateTimeOffset(new DateTime(1, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)),
                            Description = "Description-4",
                            Location = "Location-4",
                            PriorityId = 1,
                            StatusId = 1,
                            Title = "Title-4",
                            UserId = "auth0|614c239f53b183006ace3593"
                        },
                        new
                        {
                            CaseId = -5,
                            CreatedAt = new DateTimeOffset(new DateTime(1, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)),
                            Description = "Description-5",
                            Location = "Location-5",
                            PriorityId = 1,
                            StatusId = 1,
                            Title = "Title-5",
                            UserId = "auth0|614c239f53b183006ace3593"
                        });
                });

            modelBuilder.Entity("SitrepAPI.Entities.CaseImage", b =>
                {
                    b.Property<int>("CaseImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CaseImageId"), 1L, 1);

                    b.Property<int>("CaseId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
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

                    b.Property<string>("Text")
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
                            Name = "Low"
                        },
                        new
                        {
                            PriorityId = 2,
                            Name = "Medium"
                        },
                        new
                        {
                            PriorityId = 3,
                            Name = "High"
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
                            Name = "Created"
                        },
                        new
                        {
                            StatusId = 2,
                            Name = "Approved"
                        },
                        new
                        {
                            StatusId = 3,
                            Name = "Ongoing"
                        },
                        new
                        {
                            StatusId = 4,
                            Name = "Compledted"
                        },
                        new
                        {
                            StatusId = 5,
                            Name = "Decline"
                        },
                        new
                        {
                            StatusId = 6,
                            Name = "Deleted"
                        });
                });

            modelBuilder.Entity("SitrepAPI.Entities.CaseImage", b =>
                {
                    b.HasOne("SitrepAPI.Entities.Case", null)
                        .WithMany("Images")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
}
