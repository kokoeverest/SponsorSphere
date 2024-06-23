using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SponsorSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_23_06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { new DateTime(2024, 6, 23, 19, 22, 56, 859, DateTimeKind.Utc).AddTicks(4290), null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Created", "PictureId" },
                values: new object[] { new DateTime(2024, 6, 23, 19, 22, 56, 859, DateTimeKind.Utc).AddTicks(4610), null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Created", "PictureId" },
                values: new object[] { new DateTime(2024, 6, 23, 19, 22, 56, 859, DateTimeKind.Utc).AddTicks(4624), null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Created", "PictureId" },
                values: new object[] { new DateTime(2024, 6, 23, 19, 22, 56, 859, DateTimeKind.Utc).AddTicks(4513), null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Created", "PictureId" },
                values: new object[] { new DateTime(2024, 6, 23, 19, 22, 56, 859, DateTimeKind.Utc).AddTicks(4536), null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "Created", "PictureId" },
                values: new object[] { new DateTime(2024, 6, 23, 19, 22, 56, 859, DateTimeKind.Utc).AddTicks(4772), null });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Created", "PictureId" },
                values: new object[] { new DateTime(2024, 6, 23, 19, 22, 56, 859, DateTimeKind.Utc).AddTicks(4749), null });

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "Modified",
                value: new DateTime(2024, 6, 23, 19, 22, 56, 859, DateTimeKind.Utc).AddTicks(5306));

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 2,
                column: "Modified",
                value: new DateTime(2024, 6, 23, 19, 22, 56, 859, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "Sponsorships",
                keyColumns: new[] { "AthleteId", "SponsorId" },
                keyValues: new object[] { 6, 3 },
                column: "Created",
                value: new DateTime(2024, 6, 23, 19, 22, 56, 859, DateTimeKind.Utc).AddTicks(4842));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PictureId",
                table: "AspNetUsers",
                column: "PictureId",
                unique: true,
                filter: "[PictureId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Pictures_PictureId",
                table: "AspNetUsers",
                column: "PictureId",
                principalTable: "Pictures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Pictures_PictureId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PictureId",
                table: "AspNetUsers");

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
                columns: new[] { "Created", "PictureId" },
                values: new object[] { new DateTime(2024, 6, 20, 18, 4, 42, 695, DateTimeKind.Utc).AddTicks(7214), 0 });

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
        }
    }
}
