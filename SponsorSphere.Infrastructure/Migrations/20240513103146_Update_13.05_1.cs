using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SponsorSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_1305_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Country = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PictureId = table.Column<int>(type: "int", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FaceBookLink = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    InstagramLink = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TwitterLink = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StravaLink = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SportEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Country = table.Column<int>(type: "int", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Finished = table.Column<bool>(type: "bit", nullable: false),
                    EventType = table.Column<int>(type: "int", nullable: false),
                    Sport = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Athletes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Sport = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Athletes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Athletes_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogPosts_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sponsors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sponsors_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Achievements",
                columns: table => new
                {
                    SportEventId = table.Column<int>(type: "int", nullable: false),
                    AthleteId = table.Column<int>(type: "int", nullable: false),
                    Sport = table.Column<int>(type: "int", nullable: false),
                    PlaceFinished = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Achievements", x => new { x.AthleteId, x.SportEventId });
                    table.ForeignKey(
                        name: "FK_Achievements_AspNetUsers_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Achievements_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    SportEventId = table.Column<int>(type: "int", nullable: false),
                    AthleteId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sport = table.Column<int>(type: "int", nullable: false),
                    AmountNeeded = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => new { x.AthleteId, x.SportEventId });
                    table.ForeignKey(
                        name: "FK_Goals_AspNetUsers_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Goals_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BlogPostPictures",
                columns: table => new
                {
                    BlogPostId = table.Column<int>(type: "int", nullable: false),
                    PictureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostPictures", x => new { x.BlogPostId, x.PictureId });
                    table.ForeignKey(
                        name: "FK_BlogPostPictures_BlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostPictures_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SponsorCompanies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    IBAN = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SponsorCompanies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SponsorCompanies_Sponsors_Id",
                        column: x => x.Id,
                        principalTable: "Sponsors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SponsorIndividuals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SponsorIndividuals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SponsorIndividuals_Sponsors_Id",
                        column: x => x.Id,
                        principalTable: "Sponsors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sponsorships",
                columns: table => new
                {
                    AthleteId = table.Column<int>(type: "int", nullable: false),
                    SponsorId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sponsorships", x => new { x.AthleteId, x.SponsorId });
                    table.ForeignKey(
                        name: "FK_Sponsorships_AspNetUsers_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sponsorships_AspNetUsers_SponsorId",
                        column: x => x.SponsorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sponsorships_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sponsorships_Sponsors_SponsorId",
                        column: x => x.SponsorId,
                        principalTable: "Sponsors",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, null, "Admin", "ADMIN" },
                    { 2, null, "Athlete", "ATHLETE" },
                    { 3, null, "Sponsor", "SPONSOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Country", "Created", "DeletedOn", "Email", "EmailConfirmed", "FaceBookLink", "InstagramLink", "IsDeleted", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PictureId", "SecurityStamp", "StravaLink", "TwitterLink", "TwoFactorEnabled", "UserName", "Website" },
                values: new object[,]
                {
                    { 1, 0, "884782f4-6894-440f-a180-a44a65e24b86", 732800, new DateTime(2024, 5, 13, 10, 31, 45, 566, DateTimeKind.Utc).AddTicks(7768), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.admin", false, "", "", false, false, null, "admin", "ADMIN@ADMIN.ADMIN", "ADMIN@ADMIN.ADMIN", "AQAAAAIAAYagAAAAEAMtTb2zDg89LI4duiOV4i0cA2n7maewb+Zwz5cDLpp2TEUvG2X2WWxUTa8e/UKe4w==", "0123456789", false, null, "SXUZT2PQFBXQJAQUPDFAWRY6J4OWNV4K", "", "", false, "admin@admin.admin", "" },
                    { 3, 0, "7456954b-6698-44f4-8aab-51ba06ec14cf", 732800, new DateTime(2024, 5, 13, 10, 31, 45, 566, DateTimeKind.Utc).AddTicks(9367), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lidl@mail.bg", false, "", "", false, false, null, "Lidl", "LIDL@MAIL.BG", "LIDL@MAIL.BG", "AQAAAAIAAYagAAAAEAMtTb2zDg89LI4duiOV4i0cA2n7maewb+Zwz5cDLpp2TEUvG2X2WWxUTa8e/UKe4w==", "0123456789", false, null, null, "", "", false, "lidl@mail.bg", "" },
                    { 4, 0, "c72cf10a-8bd4-42d0-ae36-05757257534b", 2921044, new DateTime(2024, 5, 13, 10, 31, 45, 566, DateTimeKind.Utc).AddTicks(9397), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kaufland@bg.gb", false, "", "", false, false, null, "Kaufland", null, null, null, "0123456789", false, null, null, "", "", false, null, "" },
                    { 5, 0, "30b2c1e3-ee05-4f6c-be3c-d5ecead55417", 732800, new DateTime(2024, 5, 13, 10, 31, 45, 566, DateTimeKind.Utc).AddTicks(8884), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "test@mail.bg", false, "", "", false, false, null, "Petar", "TEST@MAIL.BG", "TEST@MAIL.BG", null, "0123456789", false, null, null, "www.strava.co/userpetar", "", false, "test@mail.bg", "" },
                    { 6, 0, "49419c45-9e03-4757-a694-f516a4fc4108", 732800, new DateTime(2024, 5, 13, 10, 31, 45, 566, DateTimeKind.Utc).AddTicks(9204), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5kov@mail.mail", false, "", "", false, false, null, "Georgi", null, null, null, "0123456789", false, null, null, "", "", false, "5kov@mail.mail", "" },
                    { 7, 0, "68aaff64-95ef-4409-8177-afd27353ff7b", 2077456, new DateTime(2024, 5, 13, 10, 31, 45, 566, DateTimeKind.Utc).AddTicks(9527), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "michael@bg.gb", false, "", "", false, false, null, "Michael", null, null, null, "1223", false, null, null, "", "", false, null, "" },
                    { 8, 0, "d71f5f99-8f8d-42d0-a6a1-ff13e0836c40", 732800, new DateTime(2024, 5, 13, 10, 31, 45, 566, DateTimeKind.Utc).AddTicks(9484), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "anonimen@bg.gb", false, "", "", false, false, null, "Lazar", null, null, null, "0123456789", false, null, null, "", "", false, null, "" }
                });

            migrationBuilder.InsertData(
                table: "Pictures",
                columns: new[] { "Id", "Content", "Modified", "Url" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 5, 13, 10, 31, 45, 566, DateTimeKind.Utc).AddTicks(9951), "https://drive.google.com/file/d/1PVTg8DDjnKEu2L_M2Oe4YBicC_Cvpy4C/view?usp=sharing" },
                    { 2, null, new DateTime(2024, 5, 13, 10, 31, 45, 566, DateTimeKind.Utc).AddTicks(9954), "https://drive.google.com/file/d/1QLGlPj9PCHBU1Lc-TQNajmHlvueoaoUG/view?usp=sharing" }
                });

            migrationBuilder.InsertData(
                table: "SportEvents",
                columns: new[] { "Id", "Country", "EventDate", "EventType", "Finished", "Name", "Sport", "Status" },
                values: new object[,]
                {
                    { 1, 732800, new DateTime(2020, 8, 15, 21, 0, 0, 0, DateTimeKind.Utc), 0, true, "Persenk ultra", 14, 0 },
                    { 2, 2510769, new DateTime(2024, 8, 15, 21, 0, 0, 0, DateTimeKind.Utc), 0, false, "Zegama Aizkori", 13, 1 }
                });

            migrationBuilder.InsertData(
                table: "Athletes",
                columns: new[] { "Id", "BirthDate", "LastName", "Sport" },
                values: new object[,]
                {
                    { 5, new DateTime(1983, 9, 29, 21, 0, 0, 0, DateTimeKind.Utc), "Petrov", 14 },
                    { 6, new DateTime(2005, 3, 29, 21, 0, 0, 0, DateTimeKind.Utc), "Petkov", 11 }
                });

            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "Id", "AuthorId", "Content", "Created" },
                values: new object[,]
                {
                    { 1, 6, "A very interesting post about a sport achievement", new DateTime(2023, 12, 5, 22, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 6, "I want to share about my experience as a downhill mountain biker. I was born in 1997 and grew up in a small villeag in the Swiss Alps. The name of the village is Zinal and it has some quite nice mountians around, which have fascinated me throughout my life!", new DateTime(2023, 12, 5, 22, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Sponsors",
                column: "Id",
                values: new object[]
                {
                    3,
                    4,
                    7,
                    8
                });

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "AthleteId", "SportEventId", "PlaceFinished", "Sport" },
                values: new object[,]
                {
                    { 5, 1, 2, 14 },
                    { 6, 1, 1, 13 }
                });

            migrationBuilder.InsertData(
                table: "BlogPostPictures",
                columns: new[] { "BlogPostId", "PictureId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "AthleteId", "SportEventId", "AmountNeeded", "Date", "Sport" },
                values: new object[] { 6, 2, 5000m, new DateTime(2024, 8, 15, 21, 0, 0, 0, DateTimeKind.Utc), 13 });

            migrationBuilder.InsertData(
                table: "SponsorCompanies",
                columns: new[] { "Id", "IBAN" },
                values: new object[,]
                {
                    { 3, "BG12345" },
                    { 4, "DE32215" }
                });

            migrationBuilder.InsertData(
                table: "SponsorIndividuals",
                columns: new[] { "Id", "BirthDate", "LastName" },
                values: new object[,]
                {
                    { 7, new DateTime(1975, 3, 29, 22, 0, 0, 0, DateTimeKind.Utc), "Uzunov" },
                    { 8, new DateTime(1990, 1, 2, 22, 0, 0, 0, DateTimeKind.Utc), "Randov" }
                });

            migrationBuilder.InsertData(
                table: "Sponsorships",
                columns: new[] { "AthleteId", "SponsorId", "Amount", "Created", "Level" },
                values: new object[] { 6, 3, 2000m, new DateTime(2024, 5, 13, 10, 31, 45, 566, DateTimeKind.Utc).AddTicks(9622), 2 });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostPictures_PictureId",
                table: "BlogPostPictures",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_AuthorId",
                table: "BlogPosts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sponsorships_SponsorId",
                table: "Sponsorships",
                column: "SponsorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BlogPostPictures");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "SponsorCompanies");

            migrationBuilder.DropTable(
                name: "SponsorIndividuals");

            migrationBuilder.DropTable(
                name: "Sponsorships");

            migrationBuilder.DropTable(
                name: "SportEvents");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "Athletes");

            migrationBuilder.DropTable(
                name: "Sponsors");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
