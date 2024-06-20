using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SponsorSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_20_06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_AspNetUsers_AuthorId",
                table: "BlogPosts");

            migrationBuilder.AlterColumn<int>(
                name: "PictureId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "PictureId" },
                values: new object[] { new DateTime(2024, 6, 20, 18, 4, 42, 695, DateTimeKind.Utc).AddTicks(7064), 0 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "PictureId" },
                values: new object[] { new DateTime(2024, 6, 20, 18, 4, 42, 695, DateTimeKind.Utc).AddTicks(7277), 0 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "PictureId" },
                values: new object[] { new DateTime(2024, 6, 20, 18, 4, 42, 695, DateTimeKind.Utc).AddTicks(7287), 0 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "PictureId", "StravaLink" },
                values: new object[] { new DateTime(2024, 6, 20, 18, 4, 42, 695, DateTimeKind.Utc).AddTicks(7214), 0, "https://www.strava.com/userpetar" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "PictureId" },
                values: new object[] { new DateTime(2024, 6, 20, 18, 4, 42, 695, DateTimeKind.Utc).AddTicks(7228), 0 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "PictureId" },
                values: new object[] { new DateTime(2024, 6, 20, 18, 4, 42, 695, DateTimeKind.Utc).AddTicks(7402), 0 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Created", "PictureId" },
                values: new object[] { new DateTime(2024, 6, 20, 18, 4, 42, 695, DateTimeKind.Utc).AddTicks(7384), 0 });

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "Modified",
                value: new DateTime(2024, 6, 20, 18, 4, 42, 695, DateTimeKind.Utc).AddTicks(7645));

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 2,
                column: "Modified",
                value: new DateTime(2024, 6, 20, 18, 4, 42, 695, DateTimeKind.Utc).AddTicks(8174));

            migrationBuilder.UpdateData(
                table: "Sponsorships",
                keyColumns: new[] { "AthleteId", "SponsorId" },
                keyValues: new object[] { 6, 3 },
                column: "Created",
                value: new DateTime(2024, 6, 20, 18, 4, 42, 695, DateTimeKind.Utc).AddTicks(7451));

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_AspNetUsers_AuthorId",
                table: "BlogPosts",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_AspNetUsers_AuthorId",
                table: "BlogPosts");

            migrationBuilder.AlterColumn<int>(
                name: "PictureId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Created", "PictureId" },
                values: new object[] { new DateTime(2024, 6, 10, 9, 23, 44, 425, DateTimeKind.Utc).AddTicks(9185), null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "PictureId" },
                values: new object[] { new DateTime(2024, 6, 10, 9, 23, 44, 425, DateTimeKind.Utc).AddTicks(9417), null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "PictureId" },
                values: new object[] { new DateTime(2024, 6, 10, 9, 23, 44, 425, DateTimeKind.Utc).AddTicks(9426), null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "PictureId", "StravaLink" },
                values: new object[] { new DateTime(2024, 6, 10, 9, 23, 44, 425, DateTimeKind.Utc).AddTicks(9347), null, "www.strava.com/userpetar" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "PictureId" },
                values: new object[] { new DateTime(2024, 6, 10, 9, 23, 44, 425, DateTimeKind.Utc).AddTicks(9362), null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "PictureId" },
                values: new object[] { new DateTime(2024, 6, 10, 9, 23, 44, 425, DateTimeKind.Utc).AddTicks(9519), null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Created", "PictureId" },
                values: new object[] { new DateTime(2024, 6, 10, 9, 23, 44, 425, DateTimeKind.Utc).AddTicks(9503), null });

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "Modified",
                value: new DateTime(2024, 6, 10, 9, 23, 44, 425, DateTimeKind.Utc).AddTicks(9783));

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 2,
                column: "Modified",
                value: new DateTime(2024, 6, 10, 9, 23, 44, 426, DateTimeKind.Utc).AddTicks(338));

            migrationBuilder.UpdateData(
                table: "Sponsorships",
                keyColumns: new[] { "AthleteId", "SponsorId" },
                keyValues: new object[] { 6, 3 },
                column: "Created",
                value: new DateTime(2024, 6, 10, 9, 23, 44, 425, DateTimeKind.Utc).AddTicks(9586));

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_AspNetUsers_AuthorId",
                table: "BlogPosts",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
