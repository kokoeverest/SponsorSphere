using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SponsorSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_0405_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "8c27d5ab-410f-41cf-9e67-be74425f3630", new DateTime(2024, 5, 4, 11, 45, 55, 104, DateTimeKind.Utc).AddTicks(5650) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "b15eb405-1b48-4186-8996-f94a83ad2405", new DateTime(2024, 5, 4, 11, 45, 55, 104, DateTimeKind.Utc).AddTicks(6104) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "2a06fc09-5a96-4be7-b92c-da43e565b1b4", new DateTime(2024, 5, 4, 11, 45, 55, 104, DateTimeKind.Utc).AddTicks(7115) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "9fca46ec-8b9e-48e4-9a46-af3a2dc33ab9", new DateTime(2024, 5, 4, 11, 45, 55, 104, DateTimeKind.Utc).AddTicks(7166) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "597daa60-6a05-4696-b4bb-9778bcede9b8", new DateTime(2024, 5, 4, 11, 45, 55, 104, DateTimeKind.Utc).AddTicks(7338) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "e80e2a84-7eed-44e5-a2cd-bc1b3aac1ca4", new DateTime(2024, 5, 4, 11, 45, 55, 104, DateTimeKind.Utc).AddTicks(7484) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ConcurrencyStamp", "Created", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "61d47f94-2709-443f-8e19-beb3c295ecef", new DateTime(2024, 5, 4, 11, 45, 55, 104, DateTimeKind.Utc).AddTicks(6194), "KALO@MAIL.BG", "KALO@MAIL.BG", "A1B2C3" });

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "Modified",
                value: new DateTime(2024, 5, 4, 11, 45, 55, 104, DateTimeKind.Utc).AddTicks(7993));

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 2,
                column: "Modified",
                value: new DateTime(2024, 5, 4, 11, 45, 55, 104, DateTimeKind.Utc).AddTicks(7995));

            migrationBuilder.UpdateData(
                table: "Sponsorships",
                keyColumns: new[] { "AthleteId", "SponsorId" },
                keyValues: new object[] { 1, 3 },
                column: "Created",
                value: new DateTime(2024, 5, 4, 11, 45, 55, 104, DateTimeKind.Utc).AddTicks(7604));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "453a2bf7-c547-4368-b27c-b9d56b623e65", new DateTime(2024, 5, 4, 10, 43, 16, 100, DateTimeKind.Utc).AddTicks(5861) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "eb264eac-3b2d-43c7-b7d8-3d79638607ef", new DateTime(2024, 5, 4, 10, 43, 16, 100, DateTimeKind.Utc).AddTicks(6348) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "30e30463-d329-4b60-953d-ab17ce623597", new DateTime(2024, 5, 4, 10, 43, 16, 100, DateTimeKind.Utc).AddTicks(7415) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "97ebfffe-d1d3-4100-8931-07b03bb46de8", new DateTime(2024, 5, 4, 10, 43, 16, 100, DateTimeKind.Utc).AddTicks(7452) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "816d8a78-8df2-485f-9573-6c841a21660d", new DateTime(2024, 5, 4, 10, 43, 16, 100, DateTimeKind.Utc).AddTicks(7553) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "e232fe81-7ef1-4619-bd96-e42199bf68dc", new DateTime(2024, 5, 4, 10, 43, 16, 100, DateTimeKind.Utc).AddTicks(7665) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ConcurrencyStamp", "Created", "NormalizedEmail", "NormalizedUserName", "PasswordHash" },
                values: new object[] { "4841cf09-b26f-4461-8015-ddda2b3c92e1", new DateTime(2024, 5, 4, 10, 43, 16, 100, DateTimeKind.Utc).AddTicks(6428), null, null, null });

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "Modified",
                value: new DateTime(2024, 5, 4, 10, 43, 16, 100, DateTimeKind.Utc).AddTicks(8180));

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 2,
                column: "Modified",
                value: new DateTime(2024, 5, 4, 10, 43, 16, 100, DateTimeKind.Utc).AddTicks(8182));

            migrationBuilder.UpdateData(
                table: "Sponsorships",
                keyColumns: new[] { "AthleteId", "SponsorId" },
                keyValues: new object[] { 1, 3 },
                column: "Created",
                value: new DateTime(2024, 5, 4, 10, 43, 16, 100, DateTimeKind.Utc).AddTicks(7777));
        }
    }
}
