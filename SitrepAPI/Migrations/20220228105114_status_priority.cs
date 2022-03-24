using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable
namespace SitrepAPI.Migrations
{
    public partial class status_priority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestClass");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CaseLogs",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "Changes",
                table: "CaseLogs",
                newName: "CreatedBy");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Cases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PriorityId",
                table: "Cases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Cases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Cases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CaseImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Priority",
                columns: table => new
                {
                    PriorityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priority", x => x.PriorityId);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });

            migrationBuilder.InsertData(
                table: "Priority",
                columns: new[] { "PriorityId", "Name" },
                values: new object[,]
                {
                    { 1, "Low" },
                    { 2, "Medium" },
                    { 3, "High" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "StatusId", "Name" },
                values: new object[,]
                {
                    { 1, "Created" },
                    { 2, "Approved" },
                    { 3, "Ongoing" },
                    { 4, "Compledted" },
                    { 5, "Decline" },
                    { 6, "Deleted" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_PriorityId",
                table: "Cases",
                column: "PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_StatusId",
                table: "Cases",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Priority_PriorityId",
                table: "Cases",
                column: "PriorityId",
                principalTable: "Priority",
                principalColumn: "PriorityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Status_StatusId",
                table: "Cases",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Priority_PriorityId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Status_StatusId",
                table: "Cases");

            migrationBuilder.DropTable(
                name: "Priority");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Cases_PriorityId",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_StatusId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "PriorityId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CaseImages");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "CaseLogs",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "CaseLogs",
                newName: "Changes");

            migrationBuilder.CreateTable(
                name: "TestClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestClass", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TestClass",
                columns: new[] { "Id", "Gender", "Name" },
                values: new object[,]
                {
                    { -5, "Gender-5", "FirstName-5" },
                    { -4, "Gender-4", "FirstName-4" },
                    { -3, "Gender-3", "FirstName-3" },
                    { -2, "Gender-2", "FirstName-2" },
                    { -1, "Gender-1", "FirstName-1" }
                });
        }
    }
}
