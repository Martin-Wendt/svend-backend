using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable
namespace SitrepAPI.Migrations
{
    public partial class logredo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "CaseLogs",
                newName: "PropertyName");

            migrationBuilder.AddColumn<string>(
                name: "NewValue",
                table: "CaseLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OldValue",
                table: "CaseLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewValue",
                table: "CaseLogs");

            migrationBuilder.DropColumn(
                name: "OldValue",
                table: "CaseLogs");

            migrationBuilder.RenameColumn(
                name: "PropertyName",
                table: "CaseLogs",
                newName: "Text");
        }
    }
}
