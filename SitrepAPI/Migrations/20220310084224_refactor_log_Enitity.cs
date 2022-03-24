using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable
namespace SitrepAPI.Migrations
{
    public partial class refactor_log_Enitity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                newName: "Message");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "CaseLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "CaseLogs");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "CaseLogs",
                newName: "PropertyName");

            migrationBuilder.AddColumn<string>(
                name: "NewValue",
                table: "CaseLogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OldValue",
                table: "CaseLogs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
#pragma warning restore
