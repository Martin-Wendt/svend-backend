using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable
namespace SitrepAPI.Migrations
{
    public partial class new_prio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Priority",
                keyColumn: "PriorityId",
                keyValue: 1,
                column: "Name",
                value: "Unset");

            migrationBuilder.UpdateData(
                table: "Priority",
                keyColumn: "PriorityId",
                keyValue: 2,
                column: "Name",
                value: "Low");

            migrationBuilder.UpdateData(
                table: "Priority",
                keyColumn: "PriorityId",
                keyValue: 3,
                column: "Name",
                value: "Medium");

            migrationBuilder.InsertData(
                table: "Priority",
                columns: new[] { "PriorityId", "Name" },
                values: new object[] { 4, "High" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Priority",
                keyColumn: "PriorityId",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Priority",
                keyColumn: "PriorityId",
                keyValue: 1,
                column: "Name",
                value: "Low");

            migrationBuilder.UpdateData(
                table: "Priority",
                keyColumn: "PriorityId",
                keyValue: 2,
                column: "Name",
                value: "Medium");

            migrationBuilder.UpdateData(
                table: "Priority",
                keyColumn: "PriorityId",
                keyValue: 3,
                column: "Name",
                value: "High");
        }
    }
}
