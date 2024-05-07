using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SponsorSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_0605 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "aa901381-1454-4dce-969c-f310b66e8036", new DateTime(2024, 5, 6, 5, 22, 50, 401, DateTimeKind.Utc).AddTicks(675) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "81d1a62d-df65-40a5-a211-5c785b001659", new DateTime(2024, 5, 6, 5, 22, 50, 401, DateTimeKind.Utc).AddTicks(1146) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "cd88808f-6382-445d-83b8-b23a09de2285", new DateTime(2024, 5, 6, 5, 22, 50, 401, DateTimeKind.Utc).AddTicks(1954) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "fc0c0447-330d-4725-b4dc-2e5788a812a2", new DateTime(2024, 5, 6, 5, 22, 50, 401, DateTimeKind.Utc).AddTicks(1973) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "3b97c2f9-8e37-4184-a424-0b9eb05bd091", new DateTime(2024, 5, 6, 5, 22, 50, 401, DateTimeKind.Utc).AddTicks(2044) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "50b5aa7f-312b-41b1-80c9-e1053060e007", new DateTime(2024, 5, 6, 5, 22, 50, 401, DateTimeKind.Utc).AddTicks(2087) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "cd4306ee-92c2-4412-bb96-52e69aa8c26d", new DateTime(2024, 5, 6, 5, 22, 50, 401, DateTimeKind.Utc).AddTicks(1324) });

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "Modified",
                value: new DateTime(2024, 5, 6, 5, 22, 50, 401, DateTimeKind.Utc).AddTicks(2505));

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 2,
                column: "Modified",
                value: new DateTime(2024, 5, 6, 5, 22, 50, 401, DateTimeKind.Utc).AddTicks(2507));

            migrationBuilder.UpdateData(
                table: "Sponsorships",
                keyColumns: new[] { "AthleteId", "SponsorId" },
                keyValues: new object[] { 1, 3 },
                column: "Created",
                value: new DateTime(2024, 5, 6, 5, 22, 50, 401, DateTimeKind.Utc).AddTicks(2184));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "61d47f94-2709-443f-8e19-beb3c295ecef", new DateTime(2024, 5, 4, 11, 45, 55, 104, DateTimeKind.Utc).AddTicks(6194) });

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
    }
}
