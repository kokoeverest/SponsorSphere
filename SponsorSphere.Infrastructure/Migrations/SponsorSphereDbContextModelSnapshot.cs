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
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
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
                            Created = new DateTime(2023, 12, 5, 22, 0, 0, 0, DateTimeKind.Utc)
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
                            Date = new DateTime(2024, 8, 15, 21, 0, 0, 0, DateTimeKind.Utc),
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
                            Modified = new DateTime(2024, 4, 30, 15, 15, 24, 46, DateTimeKind.Utc).AddTicks(5471),
                            Url = "https://drive.google.com/file/d/1PVTg8DDjnKEu2L_M2Oe4YBicC_Cvpy4C/view?usp=sharing"
                        },
                        new
                        {
                            Id = 2,
                            Modified = new DateTime(2024, 4, 30, 15, 15, 24, 46, DateTimeKind.Utc).AddTicks(5476),
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
                            Created = new DateTime(2024, 4, 30, 15, 15, 24, 46, DateTimeKind.Utc).AddTicks(4544),
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
                            EventDate = new DateTime(2020, 8, 15, 21, 0, 0, 0, DateTimeKind.Utc),
                            EventType = 0,
                            Finished = true,
                            Name = "Persenk ultra",
                            Sport = 14
                        },
                        new
                        {
                            Id = 2,
                            Country = 2510769,
                            EventDate = new DateTime(2024, 8, 15, 21, 0, 0, 0, DateTimeKind.Utc),
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

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FaceBookLink")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("InstagramLink")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("PictureId")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StravaLink")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("TwitterLink")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Website")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("SponsorSphere.Domain.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Athlete",
                            NormalizedName = "ATHLETE"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Sponsor",
                            NormalizedName = "SPONSOR"
                        });
                });

            modelBuilder.Entity("SponsorSphere.Infrastructure.BlogPostPicture", b =>
                {
                    b.Property<int>("BlogPostId")
                        .HasColumnType("int");

                    b.Property<int>("PictureId")
                        .HasColumnType("int");

                    b.HasKey("BlogPostId", "PictureId");

                    b.HasIndex("PictureId");

                    b.ToTable("BlogPostPictures", (string)null);

                    b.HasData(
                        new
                        {
                            BlogPostId = 1,
                            PictureId = 1
                        },
                        new
                        {
                            BlogPostId = 1,
                            PictureId = 2
                        });
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
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "f1599e3d-44ad-4d30-bff9-d44114cf06b6",
                            Country = 732800,
                            Created = new DateTime(2024, 4, 30, 15, 15, 24, 46, DateTimeKind.Utc).AddTicks(3632),
                            DeletedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "5rov@mail.mail",
                            EmailConfirmed = false,
                            FaceBookLink = "",
                            InstagramLink = "",
                            IsDeleted = false,
                            LockoutEnabled = false,
                            Name = "Petar",
                            PhoneNumber = "09198",
                            PhoneNumberConfirmed = false,
                            StravaLink = "",
                            TwitterLink = "",
                            TwoFactorEnabled = false,
                            Website = "",
                            BirthDate = new DateTime(1983, 9, 29, 21, 0, 0, 0, DateTimeKind.Utc),
                            LastName = "Petrov",
                            Sport = 14
                        },
                        new
                        {
                            Id = 2,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "6b7848b6-953f-4d11-8a80-2472bedda380",
                            Country = 732800,
                            Created = new DateTime(2024, 4, 30, 15, 15, 24, 46, DateTimeKind.Utc).AddTicks(3975),
                            DeletedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "5kov@mail.mail",
                            EmailConfirmed = false,
                            FaceBookLink = "",
                            InstagramLink = "",
                            IsDeleted = false,
                            LockoutEnabled = false,
                            Name = "Georgi",
                            PhoneNumber = "09198",
                            PhoneNumberConfirmed = false,
                            StravaLink = "",
                            TwitterLink = "",
                            TwoFactorEnabled = false,
                            Website = "",
                            BirthDate = new DateTime(2005, 3, 29, 21, 0, 0, 0, DateTimeKind.Utc),
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
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "ceefbd6c-5c98-4881-97d3-af6af407f431",
                            Country = 732800,
                            Created = new DateTime(2024, 4, 30, 15, 15, 24, 46, DateTimeKind.Utc).AddTicks(4438),
                            DeletedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "lidl@bg.gb",
                            EmailConfirmed = false,
                            FaceBookLink = "",
                            InstagramLink = "",
                            IsDeleted = false,
                            LockoutEnabled = false,
                            Name = "Lidl",
                            PhoneNumber = "1223",
                            PhoneNumberConfirmed = false,
                            StravaLink = "",
                            TwitterLink = "",
                            TwoFactorEnabled = false,
                            Website = "",
                            IBAN = "BG12345"
                        },
                        new
                        {
                            Id = 4,
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "931fdc5e-46ce-4b98-a0d7-f7193ffc58bc",
                            Country = 2921044,
                            Created = new DateTime(2024, 4, 30, 15, 15, 24, 46, DateTimeKind.Utc).AddTicks(4458),
                            DeletedOn = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "kaufland@bg.gb",
                            EmailConfirmed = false,
                            FaceBookLink = "",
                            InstagramLink = "",
                            IsDeleted = false,
                            LockoutEnabled = false,
                            Name = "Kaufland",
                            PhoneNumber = "1223",
                            PhoneNumberConfirmed = false,
                            StravaLink = "",
                            TwitterLink = "",
                            TwoFactorEnabled = false,
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("SponsorSphere.Domain.Models.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("SponsorSphere.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("SponsorSphere.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("SponsorSphere.Domain.Models.UserRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SponsorSphere.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("SponsorSphere.Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
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

            modelBuilder.Entity("SponsorSphere.Infrastructure.BlogPostPicture", b =>
                {
                    b.HasOne("SponsorSphere.Domain.Models.BlogPost", "BlogPost")
                        .WithMany()
                        .HasForeignKey("BlogPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SponsorSphere.Domain.Models.Picture", "Picture")
                        .WithMany()
                        .HasForeignKey("PictureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BlogPost");

                    b.Navigation("Picture");
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
