using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable
namespace SitrepAPI.Migrations
{
    public partial class isdelted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Cases",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_CaseLogs_CaseId",
                table: "CaseLogs",
                column: "CaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_CaseLogs_Cases_CaseId",
                table: "CaseLogs",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "CaseId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaseLogs_Cases_CaseId",
                table: "CaseLogs");

            migrationBuilder.DropIndex(
                name: "IX_CaseLogs_CaseId",
                table: "CaseLogs");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Cases");
        }
    }
}
