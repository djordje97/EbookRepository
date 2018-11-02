using Microsoft.EntityFrameworkCore.Migrations;

namespace EBookStore.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[] { 1, "Comedy" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CategoryId", "Firstname", "Lastname", "Password", "Type", "Username" },
                values: new object[] { 1, 1, "Marko", "Markovic", "123", "Admin", "marko" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);
        }
    }
}
