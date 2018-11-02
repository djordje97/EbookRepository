using Microsoft.EntityFrameworkCore.Migrations;

namespace EBookStore.Migrations
{
    public partial class SeedDataa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "AQAAAAEAACcQAAAAEHW+Ld3xxYOGBJt8cIFvrOyMJlnRE4RCjGlYMHuVQXZyoxin5ZlWSVAOeNlwgPNtuw==");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "CategoryId", "Firstname", "Lastname", "Password", "Type", "Username" },
                values: new object[] { 2, 1, "Darko", "Stankic", "AQAAAAEAACcQAAAAECQNK447tZzsRB2TLklc6vEmyPFovgOEMrpl7nKBLPLndi3zKKFRwhQEEdgmeEAMzA==", "User", "darko" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "Password",
                value: "123");
        }
    }
}
