using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable
namespace SitrepAPI.Migrations
{
    public partial class caselog_redo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaseLogs_Cases_CaseId",
                table: "CaseLogs");

            migrationBuilder.DropIndex(
                name: "IX_CaseLogs_CaseId",
                table: "CaseLogs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
