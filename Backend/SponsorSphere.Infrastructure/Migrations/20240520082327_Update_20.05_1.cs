using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SponsorSphere.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update_2005_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 5, 20, 8, 23, 26, 190, DateTimeKind.Utc).AddTicks(5137));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "1f18c34c-96a9-4105-8883-4164bfdafd16", new DateTime(2024, 5, 20, 8, 23, 26, 190, DateTimeKind.Utc).AddTicks(5511) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "0fa3db38-0a2a-4fbb-96bd-0e5e4d5083cb", new DateTime(2024, 5, 20, 8, 23, 26, 190, DateTimeKind.Utc).AddTicks(5532) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "4a32d924-6ecd-417a-94f0-b83f17efb5d8", new DateTime(2024, 5, 20, 8, 23, 26, 190, DateTimeKind.Utc).AddTicks(5350) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "0ae2ab33-20db-4338-868a-8ef0ded46bea", new DateTime(2024, 5, 20, 8, 23, 26, 190, DateTimeKind.Utc).AddTicks(5383) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "5b3c3d65-8e27-485a-aeb0-453b0c8c18af", new DateTime(2024, 5, 20, 8, 23, 26, 190, DateTimeKind.Utc).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "f09fccf6-e7d1-4c25-bd9e-cf4c42114f59", new DateTime(2024, 5, 20, 8, 23, 26, 190, DateTimeKind.Utc).AddTicks(5614) });

            migrationBuilder.UpdateData(
                table: "Athletes",
                keyColumn: "Id",
                keyValue: 5,
                column: "BirthDate",
                value: new DateTime(1983, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Athletes",
                keyColumn: "Id",
                keyValue: 6,
                column: "BirthDate",
                value: new DateTime(2005, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2016, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Goals",
                keyColumns: new[] { "AthleteId", "SportEventId" },
                keyValues: new object[] { 6, 2 },
                column: "Date",
                value: new DateTime(2024, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "Modified",
                value: new DateTime(2024, 5, 20, 8, 23, 26, 190, DateTimeKind.Utc).AddTicks(6190));

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 2,
                column: "Modified",
                value: new DateTime(2024, 5, 20, 8, 23, 26, 190, DateTimeKind.Utc).AddTicks(6192));

            migrationBuilder.UpdateData(
                table: "SponsorIndividuals",
                keyColumn: "Id",
                keyValue: 7,
                column: "BirthDate",
                value: new DateTime(1975, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SponsorIndividuals",
                keyColumn: "Id",
                keyValue: 8,
                column: "BirthDate",
                value: new DateTime(1990, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Sponsorships",
                keyColumns: new[] { "AthleteId", "SponsorId" },
                keyValues: new object[] { 6, 3 },
                column: "Created",
                value: new DateTime(2024, 5, 20, 8, 23, 26, 190, DateTimeKind.Utc).AddTicks(5695));

            migrationBuilder.UpdateData(
                table: "SportEvents",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2020, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SportEvents",
                keyColumn: "Id",
                keyValue: 2,
                column: "EventDate",
                value: new DateTime(2024, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "SportEvents",
                keyColumn: "Id",
                keyValue: 3,
                column: "EventDate",
                value: new DateTime(2019, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2024, 5, 20, 8, 15, 13, 543, DateTimeKind.Utc).AddTicks(4251));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "8ae18837-671f-44fa-89b2-5033585a40ca", new DateTime(2024, 5, 20, 8, 15, 13, 543, DateTimeKind.Utc).AddTicks(4716) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "d4a4ebf8-9461-4115-817f-0287352e2b60", new DateTime(2024, 5, 20, 8, 15, 13, 543, DateTimeKind.Utc).AddTicks(4732) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "32e89955-4020-4c65-9783-d8e5ed1d43df", new DateTime(2024, 5, 20, 8, 15, 13, 543, DateTimeKind.Utc).AddTicks(4442) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "a4476200-3720-4919-95e7-0311068f841e", new DateTime(2024, 5, 20, 8, 15, 13, 543, DateTimeKind.Utc).AddTicks(4633) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "a632f973-3b32-4980-b844-f079afb1a2fb", new DateTime(2024, 5, 20, 8, 15, 13, 543, DateTimeKind.Utc).AddTicks(4817) });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "ConcurrencyStamp", "Created" },
                values: new object[] { "bfa688d5-9e73-49e6-8944-87d3fc924397", new DateTime(2024, 5, 20, 8, 15, 13, 543, DateTimeKind.Utc).AddTicks(4785) });

            migrationBuilder.UpdateData(
                table: "Athletes",
                keyColumn: "Id",
                keyValue: 5,
                column: "BirthDate",
                value: new DateTime(1983, 9, 29, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Athletes",
                keyColumn: "Id",
                keyValue: 6,
                column: "BirthDate",
                value: new DateTime(2005, 3, 29, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "Created",
                value: new DateTime(2016, 6, 11, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 2,
                column: "Created",
                value: new DateTime(2023, 12, 5, 22, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Goals",
                keyColumns: new[] { "AthleteId", "SportEventId" },
                keyValues: new object[] { 6, 2 },
                column: "Date",
                value: new DateTime(2024, 8, 15, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "Modified",
                value: new DateTime(2024, 5, 20, 8, 15, 13, 543, DateTimeKind.Utc).AddTicks(5078));

            migrationBuilder.UpdateData(
                table: "Pictures",
                keyColumn: "Id",
                keyValue: 2,
                column: "Modified",
                value: new DateTime(2024, 5, 20, 8, 15, 13, 543, DateTimeKind.Utc).AddTicks(5080));

            migrationBuilder.UpdateData(
                table: "SponsorIndividuals",
                keyColumn: "Id",
                keyValue: 7,
                column: "BirthDate",
                value: new DateTime(1975, 3, 4, 22, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "SponsorIndividuals",
                keyColumn: "Id",
                keyValue: 8,
                column: "BirthDate",
                value: new DateTime(1990, 1, 2, 22, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Sponsorships",
                keyColumns: new[] { "AthleteId", "SponsorId" },
                keyValues: new object[] { 6, 3 },
                column: "Created",
                value: new DateTime(2024, 5, 20, 8, 15, 13, 543, DateTimeKind.Utc).AddTicks(4890));

            migrationBuilder.UpdateData(
                table: "SportEvents",
                keyColumn: "Id",
                keyValue: 1,
                column: "EventDate",
                value: new DateTime(2020, 8, 15, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "SportEvents",
                keyColumn: "Id",
                keyValue: 2,
                column: "EventDate",
                value: new DateTime(2024, 8, 15, 21, 0, 0, 0, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "SportEvents",
                keyColumn: "Id",
                keyValue: 3,
                column: "EventDate",
                value: new DateTime(2019, 9, 8, 21, 0, 0, 0, DateTimeKind.Utc));
        }
    }
}
