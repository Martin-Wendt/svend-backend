using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable

namespace SitrepAPI.Migrations
{
    public partial class Casestructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cases",
                columns: table => new
                {
                    CaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.CaseId);
                });

            migrationBuilder.CreateTable(
                name: "CaseImages",
                columns: table => new
                {
                    CaseImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CaseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseImages", x => x.CaseImageId);
                    table.ForeignKey(
                        name: "FK_CaseImages_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "CaseId");
                });

            migrationBuilder.CreateTable(
                name: "CaseLogs",
                columns: table => new
                {
                    CaseLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Changes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseLogs", x => x.CaseLogId);
                    table.ForeignKey(
                        name: "FK_CaseLogs_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "CaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaseImages_CaseId",
                table: "CaseImages",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseLogs_CaseId",
                table: "CaseLogs",
                column: "CaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseImages");

            migrationBuilder.DropTable(
                name: "CaseLogs");

            migrationBuilder.DropTable(
                name: "Cases");
        }
    }
}
