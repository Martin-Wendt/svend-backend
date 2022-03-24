using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable
namespace SitrepAPI.Migrations
{
    public partial class entities_relation_chantes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "CaseId",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "CaseId",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "CaseId",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "CaseId",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Cases",
                keyColumn: "CaseId",
                keyValue: -1);

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

            migrationBuilder.DropIndex(
                name: "IX_Cases_PriorityId",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_StatusId",
                table: "Cases");

            migrationBuilder.InsertData(
                table: "Cases",
                columns: new[] { "CaseId", "CreatedAt", "Description", "Location", "PriorityId", "StatusId", "Title", "UserId" },
                values: new object[,]
                {
                    { -5, new DateTimeOffset(new DateTime(1, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "Description-5", "Location-5", 1, 1, "Title-5", "auth0|614c239f53b183006ace3593" },
                    { -4, new DateTimeOffset(new DateTime(1, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "Description-4", "Location-4", 1, 1, "Title-4", "auth0|614c239f53b183006ace3593" },
                    { -3, new DateTimeOffset(new DateTime(1, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "Description-3", "Location-3", 1, 1, "Title-3", "auth0|614c239f53b183006ace3593" },
                    { -2, new DateTimeOffset(new DateTime(1, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Description-2", "Location-2", 1, 1, "Title-2", "auth0|614c239f53b183006ace3593" },
                    { -1, new DateTimeOffset(new DateTime(1, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 2, 0, 0, 0)), "Description-1", "Location-1", 1, 1, "Title-1", "auth0|614c239f53b183006ace3593" }
                });
        }
    }
}
