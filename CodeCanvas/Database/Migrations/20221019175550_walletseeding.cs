using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeCanvas.Database.Migrations
{
    public partial class walletseeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "CreatedAt", "CurrencyCode", "UpdatedAt" },
                values: new object[] { 1, 0m, new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Local), "USD", new DateTime(2022, 10, 19, 0, 0, 0, 0, DateTimeKind.Local) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
