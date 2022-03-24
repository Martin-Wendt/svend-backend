using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable
namespace SitrepAPI.Migrations
{
    public partial class assignee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Assignee",
                table: "Cases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Assignee",
                table: "Cases");
        }
    }
}
