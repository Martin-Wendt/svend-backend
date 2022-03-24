using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable

namespace SitrepAPI.Migrations
{
    public partial class caseImage_refactor2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaseImages_Cases_CaseId",
                table: "CaseImages");

            migrationBuilder.AlterColumn<int>(
                name: "CaseId",
                table: "CaseImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CaseImages_Cases_CaseId",
                table: "CaseImages",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "CaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaseImages_Cases_CaseId",
                table: "CaseImages");

            migrationBuilder.AlterColumn<int>(
                name: "CaseId",
                table: "CaseImages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CaseImages_Cases_CaseId",
                table: "CaseImages",
                column: "CaseId",
                principalTable: "Cases",
                principalColumn: "CaseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
#pragma warning restore

