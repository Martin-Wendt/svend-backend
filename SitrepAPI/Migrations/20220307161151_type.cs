using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable
namespace SitrepAPI.Migrations
{
    public partial class type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: 4,
                column: "Name",
                value: "Completed");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: 4,
                column: "Name",
                value: "Compledted");
        }
    }
}
