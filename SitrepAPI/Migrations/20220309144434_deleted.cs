using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable
namespace SitrepAPI.Migrations
{
    public partial class deleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Cases");

            migrationBuilder.UpdateData(
                table: "Priority",
                keyColumn: "PriorityId",
                keyValue: 2,
                column: "Name",
                value: "Lav");

            migrationBuilder.UpdateData(
                table: "Priority",
                keyColumn: "PriorityId",
                keyValue: 3,
                column: "Name",
                value: "Mellem");

            migrationBuilder.UpdateData(
                table: "Priority",
                keyColumn: "PriorityId",
                keyValue: 4,
                column: "Name",
                value: "Høj");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: 1,
                column: "Name",
                value: "Oprettet");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: 2,
                column: "Name",
                value: "Godkendt");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: 3,
                column: "Name",
                value: "Igangværende");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: 4,
                column: "Name",
                value: "Afsluttet");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: 5,
                column: "Name",
                value: "Afvist");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: 6,
                column: "Name",
                value: "Slettet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Cases",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.UpdateData(
                table: "Priority",
                keyColumn: "PriorityId",
                keyValue: 4,
                column: "Name",
                value: "High");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: 1,
                column: "Name",
                value: "Created");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: 2,
                column: "Name",
                value: "Approved");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: 3,
                column: "Name",
                value: "Ongoing");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: 4,
                column: "Name",
                value: "Completed");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: 5,
                column: "Name",
                value: "Decline");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: 6,
                column: "Name",
                value: "Deleted");
        }
    }
}
#pragma warning restore