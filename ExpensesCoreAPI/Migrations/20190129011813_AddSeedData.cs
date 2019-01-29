using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpensesCoreAPI.Migrations
{
    public partial class AddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Username" },
                values: new object[] { 1, "UserAlpha" });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "TransactionID", "CreditAmount", "DebitAmount", "Description", "TransactionDate", "UserID" },
                values: new object[] { 1, 0.0, 34.5, "misc", "2019-01-01", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "TransactionID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1);
        }
    }
}
