using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable

namespace SitrepAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestClass");
        }
    }
}
