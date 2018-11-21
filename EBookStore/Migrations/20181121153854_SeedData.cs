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
                values: new object[] { 1, "None" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[] { 2, "Comedy" });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "LanguageId", "Name" },
                values: new object[] { 1, "Serbian" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CategoryId", "Firstname", "Lastname", "Password", "Type", "Username" },
                values: new object[] { 1, 1, "Marko", "Markovic", "AQAAAAEAACcQAAAAEH07BVTixP4CRndI3496bymdZWTYRBn07B8XqtW4gKWS4Wk0sZzgycd9IV6YYvldhw==", "Admin", "marko" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CategoryId", "Firstname", "Lastname", "Password", "Type", "Username" },
                values: new object[] { 2, 1, "Darko", "Stankic", "AQAAAAEAACcQAAAAED+taTt3FKCn9IBWlyjovonTgACz+FE+AbSy3JaDqyhAQytvABSPa/Z4SeFDeYekHg==", "User", "darko" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "LanguageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);
        }
    }
}
