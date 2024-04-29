using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SponsorSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update2904_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<byte>(type: "tinyint", nullable: true),
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
                    Sport = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SportEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
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
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Athletes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Sport = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Athletes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Athletes_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
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
                        name: "FK_BlogPosts_Users_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Users",
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
                        name: "FK_Sponsors_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
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
                        name: "FK_Achievements_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Achievements_Users_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_Goals_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Goals_Users_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostPicture",
                columns: table => new
                {
                    BlogPostsId = table.Column<int>(type: "int", nullable: false),
                    PicturesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostPicture", x => new { x.BlogPostsId, x.PicturesId });
                    table.ForeignKey(
                        name: "FK_BlogPostPicture_BlogPosts_BlogPostsId",
                        column: x => x.BlogPostsId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostPicture_Pictures_PicturesId",
                        column: x => x.PicturesId,
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
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                        name: "FK_Sponsorships_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sponsorships_Sponsors_SponsorId",
                        column: x => x.SponsorId,
                        principalTable: "Sponsors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sponsorships_Users_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sponsorships_Users_SponsorId",
                        column: x => x.SponsorId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "BlogPostPictures",
                columns: new[] { "BlogPostId", "PictureId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "Pictures",
                columns: new[] { "Id", "Content", "Modified", "Url" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2024, 4, 29, 3, 20, 10, 976, DateTimeKind.Utc).AddTicks(4793), "https://drive.google.com/file/d/1PVTg8DDjnKEu2L_M2Oe4YBicC_Cvpy4C/view?usp=sharing" },
                    { 2, null, new DateTime(2024, 4, 29, 3, 20, 10, 976, DateTimeKind.Utc).AddTicks(4794), "https://drive.google.com/file/d/1QLGlPj9PCHBU1Lc-TQNajmHlvueoaoUG/view?usp=sharing" }
                });

            migrationBuilder.InsertData(
                table: "SportEvents",
                columns: new[] { "Id", "Country", "EventDate", "EventType", "Finished", "Name", "Sport" },
                values: new object[,]
                {
                    { 1, 732800, new DateTime(2020, 8, 15, 21, 0, 0, 0, DateTimeKind.Utc), 0, true, "Persenk ultra", 14 },
                    { 2, 2510769, new DateTime(2024, 8, 15, 21, 0, 0, 0, DateTimeKind.Utc), 0, false, "Zegama Aizkori", 13 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Country", "Created", "DeletedOn", "Email", "FaceBookLink", "InstagramLink", "IsDeleted", "Name", "Password", "PhoneNumber", "PictureId", "StravaLink", "TwitterLink", "Website" },
                values: new object[,]
                {
                    { 1, 732800, new DateTime(2024, 4, 29, 3, 20, 10, 976, DateTimeKind.Utc).AddTicks(4135), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5rov@mail.mail", "", "", false, "Petar", "dd", "09198", null, "", "", "" },
                    { 2, 732800, new DateTime(2024, 4, 29, 3, 20, 10, 976, DateTimeKind.Utc).AddTicks(4311), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5kov@mail.mail", "", "", false, "Georgi", "ss", "09198", null, "", "", "" },
                    { 3, 732800, new DateTime(2024, 4, 29, 3, 20, 10, 976, DateTimeKind.Utc).AddTicks(4550), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lidl@bg.gb", "", "", false, "Lidl", "ll", "1223", null, "", "", "" },
                    { 4, 2921044, new DateTime(2024, 4, 29, 3, 20, 10, 976, DateTimeKind.Utc).AddTicks(4553), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "kaufland@bg.gb", "", "", false, "Kaufland", "kk", "1223", null, "", "", "" }
                });

            migrationBuilder.InsertData(
                table: "Athletes",
                columns: new[] { "Id", "BirthDate", "LastName", "Sport" },
                values: new object[,]
                {
                    { 1, new DateTime(1983, 9, 29, 21, 0, 0, 0, DateTimeKind.Utc), "Petrov", 14 },
                    { 2, new DateTime(2005, 3, 29, 21, 0, 0, 0, DateTimeKind.Utc), "Petkov", 4 }
                });

            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "Id", "AuthorId", "Content", "Created" },
                values: new object[] { 1, 4, "A very interesting post about a sport achievement", new DateTime(2023, 12, 5, 22, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "Sponsors",
                column: "Id",
                values: new object[]
                {
                    3,
                    4
                });

            migrationBuilder.InsertData(
                table: "Achievements",
                columns: new[] { "AthleteId", "SportEventId", "PlaceFinished", "Sport" },
                values: new object[,]
                {
                    { 1, 1, 2, 14 },
                    { 2, 1, 1, 13 }
                });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "AthleteId", "SportEventId", "AmountNeeded", "Date", "Sport" },
                values: new object[] { 2, 2, 5000m, new DateTime(2024, 8, 15, 21, 0, 0, 0, DateTimeKind.Utc), 13 });

            migrationBuilder.InsertData(
                table: "SponsorCompanies",
                columns: new[] { "Id", "IBAN" },
                values: new object[,]
                {
                    { 3, "BG12345" },
                    { 4, "DE32215" }
                });

            migrationBuilder.InsertData(
                table: "Sponsorships",
                columns: new[] { "AthleteId", "SponsorId", "Amount", "Created", "Level" },
                values: new object[] { 1, 3, 2000m, new DateTime(2024, 4, 29, 3, 20, 10, 976, DateTimeKind.Utc).AddTicks(4587), 2 });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostPicture_PicturesId",
                table: "BlogPostPicture",
                column: "PicturesId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_AuthorId",
                table: "BlogPosts",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sponsorships_SponsorId",
                table: "Sponsorships",
                column: "SponsorId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Achievements");

            migrationBuilder.DropTable(
                name: "BlogPostPicture");

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
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "Athletes");

            migrationBuilder.DropTable(
                name: "Sponsors");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
