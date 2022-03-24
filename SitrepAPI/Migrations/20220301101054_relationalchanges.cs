using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable
namespace SitrepAPI.Migrations
{
    public partial class relationalchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "CaseId",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "CaseId",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "CaseId",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "CaseId",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "CaseId",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "CaseId",
                keyValue: -5,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "CaseId",
                keyValue: -4,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 6, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "CaseId",
                keyValue: -3,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "CaseId",
                keyValue: -2,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Cases",
                keyColumn: "CaseId",
                keyValue: -1,
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(1, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)));
        }
    }
}
