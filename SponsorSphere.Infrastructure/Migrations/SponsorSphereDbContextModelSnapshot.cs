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

            modelBuilder.Entity("BlogPostPicture", b =>
                {
                    b.Property<int>("BlogPostsId")
                        .HasColumnType("int");

                    b.Property<int>("PicturesId")
                        .HasColumnType("int");

                    b.HasKey("BlogPostsId", "PicturesId");

                    b.HasIndex("PicturesId");

                    b.ToTable("BlogPostPicture");
                });

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

                    b.ToTable("Achievements");

                    b.HasData(
                        new
                        {
                            AthleteId = 2,
                            SportEventId = 1,
                            PlaceFinished = 1,
                            Sport = 13
                        },
                        new
                        {
                            AthleteId = 1,
                            SportEventId = 1,
                            PlaceFinished = 2,
                            Sport = 14
                        });
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.BlogPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("BlogPosts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AuthorId = 4,
                            Content = "A very interesting post about a sport achievement",
                            Created = new DateTime(2023, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
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

                    b.ToTable("Goals");

                    b.HasData(
                        new
                        {
                            AthleteId = 2,
                            SportEventId = 2,
                            AmountNeeded = 5000m,
                            Date = new DateTime(2024, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Sport = 13
                        });
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.Picture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte?>("Content")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("Modified")
                        .HasColumnType("datetime2");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pictures");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Modified = new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Local),
                            Url = "https://drive.google.com/file/d/1PVTg8DDjnKEu2L_M2Oe4YBicC_Cvpy4C/view?usp=sharing"
                        },
                        new
                        {
                            Id = 2,
                            Modified = new DateTime(2024, 4, 26, 0, 0, 0, 0, DateTimeKind.Local),
                            Url = "https://drive.google.com/file/d/1QLGlPj9PCHBU1Lc-TQNajmHlvueoaoUG/view?usp=sharing"
                        });
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

                    b.HasData(
                        new
                        {
                            AthleteId = 1,
                            SponsorId = 3,
                            Amount = 2000m,
                            Created = new DateTime(2024, 4, 26, 18, 18, 12, 653, DateTimeKind.Local).AddTicks(9499),
                            Level = 2
                        });
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.SportEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Country")
                        .HasColumnType("int");

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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = 732800,
                            EventDate = new DateTime(2020, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventType = 0,
                            Finished = true,
                            Name = "Persenk ultra",
                            Sport = 14
                        },
                        new
                        {
                            Id = 2,
                            Country = 2510769,
                            EventDate = new DateTime(2024, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EventType = 0,
                            Finished = false,
                            Name = "Zegama Aizkori",
                            Sport = 13
                        });
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Country")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DeletedOn")
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

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

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

                    b.Property<int?>("PictureId")
                        .HasColumnType("int");

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

                    b.HasIndex("Email")
                        .IsUnique();

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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = 732800,
                            Created = new DateTime(2024, 4, 26, 18, 18, 12, 653, DateTimeKind.Local).AddTicks(9057),
                            DeletedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "5rov@mail.mail",
                            FaceBookLink = "",
                            InstagramLink = "",
                            IsDeleted = false,
                            Name = "Petar",
                            Password = "dd",
                            PhoneNumber = "09198",
                            StravaLink = "",
                            TwitterLink = "",
                            Website = "",
                            BirthDate = new DateTime(1983, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Petrov",
                            Sport = 14
                        },
                        new
                        {
                            Id = 2,
                            Country = 732800,
                            Created = new DateTime(2024, 4, 26, 18, 18, 12, 653, DateTimeKind.Local).AddTicks(9196),
                            DeletedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "5kov@mail.mail",
                            FaceBookLink = "",
                            InstagramLink = "",
                            IsDeleted = false,
                            Name = "Georgi",
                            Password = "ss",
                            PhoneNumber = "09198",
                            StravaLink = "",
                            TwitterLink = "",
                            Website = "",
                            BirthDate = new DateTime(2005, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Petkov",
                            Sport = 4
                        });
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.Sponsor", b =>
                {
                    b.HasBaseType("SponsorSphere.Domain.Models.User");

                    b.ToTable("Sponsors", (string)null);
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.SponsorCompany", b =>
                {
                    b.HasBaseType("SponsorSphere.Domain.Models.Sponsor");

                    b.Property<string>("IBAN")
                        .IsRequired()
                        .HasMaxLength(34)
                        .HasColumnType("nvarchar(34)");

                    b.ToTable("SponsorCompanies");

                    b.HasData(
                        new
                        {
                            Id = 3,
                            Country = 732800,
                            Created = new DateTime(2024, 4, 26, 18, 18, 12, 653, DateTimeKind.Local).AddTicks(9447),
                            DeletedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "lidl@bg.gb",
                            FaceBookLink = "",
                            InstagramLink = "",
                            IsDeleted = false,
                            Name = "Lidl",
                            Password = "ll",
                            PhoneNumber = "1223",
                            StravaLink = "",
                            TwitterLink = "",
                            Website = "",
                            IBAN = "BG12345"
                        },
                        new
                        {
                            Id = 4,
                            Country = 732800,
                            Created = new DateTime(2024, 4, 26, 18, 18, 12, 653, DateTimeKind.Local).AddTicks(9455),
                            DeletedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "kaufland@bg.gb",
                            FaceBookLink = "",
                            InstagramLink = "",
                            IsDeleted = false,
                            Name = "Kaufland",
                            Password = "kk",
                            PhoneNumber = "1223",
                            StravaLink = "",
                            TwitterLink = "",
                            Website = "",
                            IBAN = "DE32215"
                        });
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.SponsorIndividual", b =>
                {
                    b.HasBaseType("SponsorSphere.Domain.Models.Sponsor");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.ToTable("SponsorIndividuals");
                });

            modelBuilder.Entity("BlogPostPicture", b =>
                {
                    b.HasOne("SponsorSphere.Domain.Models.BlogPost", null)
                        .WithMany()
                        .HasForeignKey("BlogPostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SponsorSphere.Domain.Models.Picture", null)
                        .WithMany()
                        .HasForeignKey("PicturesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.Achievement", b =>
                {
                    b.HasOne("SponsorSphere.Domain.Models.Athlete", null)
                        .WithMany("Achievements")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SponsorSphere.Domain.Models.User", "Athlete")
                        .WithMany()
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Athlete");
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.BlogPost", b =>
                {
                    b.HasOne("SponsorSphere.Domain.Models.User", "Author")
                        .WithMany("BlogPosts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.Goal", b =>
                {
                    b.HasOne("SponsorSphere.Domain.Models.Athlete", null)
                        .WithMany("Goals")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SponsorSphere.Domain.Models.User", "Athlete")
                        .WithMany()
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Athlete");
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.Sponsorship", b =>
                {
                    b.HasOne("SponsorSphere.Domain.Models.Athlete", null)
                        .WithMany("Sponsorships")
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SponsorSphere.Domain.Models.User", "Athlete")
                        .WithMany()
                        .HasForeignKey("AthleteId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("SponsorSphere.Domain.Models.Sponsor", null)
                        .WithMany("Sponsorships")
                        .HasForeignKey("SponsorId")
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

            modelBuilder.Entity("SponsorSphere.Domain.Models.Sponsor", b =>
                {
                    b.HasOne("SponsorSphere.Domain.Models.User", null)
                        .WithOne()
                        .HasForeignKey("SponsorSphere.Domain.Models.Sponsor", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.SponsorCompany", b =>
                {
                    b.HasOne("SponsorSphere.Domain.Models.Sponsor", null)
                        .WithOne()
                        .HasForeignKey("SponsorSphere.Domain.Models.SponsorCompany", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.SponsorIndividual", b =>
                {
                    b.HasOne("SponsorSphere.Domain.Models.Sponsor", null)
                        .WithOne()
                        .HasForeignKey("SponsorSphere.Domain.Models.SponsorIndividual", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.User", b =>
                {
                    b.Navigation("BlogPosts");
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.Athlete", b =>
                {
                    b.Navigation("Achievements");

                    b.Navigation("Goals");

                    b.Navigation("Sponsorships");
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.Sponsor", b =>
                {
                    b.Navigation("Sponsorships");
                });
#pragma warning restore 612, 618
        }
    }
}
