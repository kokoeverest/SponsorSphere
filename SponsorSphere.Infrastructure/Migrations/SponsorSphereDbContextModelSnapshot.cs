﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SponsorSphere.Infrastructure;

#nullable disable

namespace SponsorSphere.Infrastructure.Migrations
{
    [DbContext(typeof(SponsorSphereDbContext))]
    partial class SponsorSphereDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SponsorSphere.Domain.Models.Achievement", b =>
                {
                    b.Property<int>("AthleteId")
                        .HasColumnType("int");

                    b.Property<int>("SportEventId")
                        .HasColumnType("int");

                    b.Property<int?>("PlaceFinished")
                        .HasColumnType("int");

                    b.Property<int>("Sport")
                        .HasColumnType("int");

                    b.HasKey("AthleteId", "SportEventId");

                    b.HasIndex("SportEventId")
                        .IsUnique();

                    b.ToTable("Achievements");
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.BlogPost", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Pictures")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.Goal", b =>
                {
                    b.Property<int>("AthleteId")
                        .HasColumnType("int");

                    b.Property<int>("SportEventId")
                        .HasColumnType("int");

                    b.Property<decimal>("AmountNeeded")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Sport")
                        .HasColumnType("int");

                    b.HasKey("AthleteId", "SportEventId");

                    b.HasIndex("SportEventId")
                        .IsUnique();

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.Sponsorship", b =>
                {
                    b.Property<int>("AthleteId")
                        .HasColumnType("int");

                    b.Property<int>("SponsorId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.HasKey("AthleteId", "SponsorId");

                    b.HasIndex("SponsorId");

                    b.ToTable("Sponsorships");
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.SportEvent", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("EventDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EventType")
                        .HasColumnType("int");

                    b.Property<bool>("Finished")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Sport")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SportEvents");
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.User", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FaceBookLink")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("InstagramLink")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("PictureOrLogo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StravaLink")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("TwitterLink")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Website")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.Athlete", b =>
                {
                    b.HasBaseType("SponsorSphere.Domain.Models.User");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Sport")
                        .HasColumnType("int");

                    b.ToTable("Athletes");
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.SponsorCompany", b =>
                {
                    b.HasBaseType("SponsorSphere.Domain.Models.User");

                    b.Property<string>("IBAN")
                        .IsRequired()
                        .HasMaxLength(34)
                        .HasColumnType("nvarchar(34)");

                    b.ToTable("SponsorCompanies");
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.SponsorIndividual", b =>
                {
                    b.HasBaseType("SponsorSphere.Domain.Models.User");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.ToTable("SponsorIndividuals");
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.Achievement", b =>
                {
                    b.HasOne("SponsorSphere.Domain.Models.Athlete", "Athlete")
                        .WithMany("Achievements")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SponsorSphere.Domain.Models.SportEvent", "SportEvent")
                        .WithOne()
                        .HasForeignKey("SponsorSphere.Domain.Models.Achievement", "SportEventId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Athlete");

                    b.Navigation("SportEvent");
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.BlogPost", b =>
                {
                    b.HasOne("SponsorSphere.Domain.Models.User", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.Goal", b =>
                {
                    b.HasOne("SponsorSphere.Domain.Models.Athlete", "Athlete")
                        .WithMany("Goals")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SponsorSphere.Domain.Models.SportEvent", "SportEvent")
                        .WithOne()
                        .HasForeignKey("SponsorSphere.Domain.Models.Goal", "SportEventId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Athlete");

                    b.Navigation("SportEvent");
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.Sponsorship", b =>
                {
                    b.HasOne("SponsorSphere.Domain.Models.Athlete", "Athlete")
                        .WithMany()
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SponsorSphere.Domain.Models.User", "Sponsor")
                        .WithMany()
                        .HasForeignKey("SponsorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Athlete");

                    b.Navigation("Sponsor");
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.Athlete", b =>
                {
                    b.HasOne("SponsorSphere.Domain.Models.User", null)
                        .WithOne()
                        .HasForeignKey("SponsorSphere.Domain.Models.Athlete", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.SponsorCompany", b =>
                {
                    b.HasOne("SponsorSphere.Domain.Models.User", null)
                        .WithOne()
                        .HasForeignKey("SponsorSphere.Domain.Models.SponsorCompany", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.SponsorIndividual", b =>
                {
                    b.HasOne("SponsorSphere.Domain.Models.User", null)
                        .WithOne()
                        .HasForeignKey("SponsorSphere.Domain.Models.SponsorIndividual", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.User", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.Athlete", b =>
                {
                    b.Navigation("Achievements");

                    b.Navigation("Goals");
                });
#pragma warning restore 612, 618
        }
    }
}
