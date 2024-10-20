using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBots.Migrations
{
    /// <inheritdoc />
    public partial class RenameExchangeToExchangeAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TradingBot_Exchanges_ExchangeId",
                table: "TradingBot");

            migrationBuilder.RenameTable(
                name: "Exchanges",
                newName: "ExchangeAccounts");

            migrationBuilder.RenameColumn(
                name: "ExchangeId",
                table: "TradingBot",
                newName: "ExchangeAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_TradingBot_ExchangeId",
                table: "TradingBot",
                newName: "IX_TradingBot_ExchangeAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_TradingBot_ExchangeAccounts_ExchangeAccountId",
                table: "TradingBot",
                column: "ExchangeAccountId",
                principalTable: "ExchangeAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TradingBot_ExchangeAccounts_ExchangeAccountId",
                table: "TradingBot");

            migrationBuilder.RenameColumn(
                name: "ExchangeAccountId",
                table: "TradingBot",
                newName: "ExchangeId");

            migrationBuilder.RenameIndex(
                name: "IX_TradingBot_ExchangeAccountId",
                table: "TradingBot",
                newName: "IX_TradingBot_ExchangeId");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Exchanges");

            migrationBuilder.AddForeignKey(
                name: "FK_TradingBot_Exchanges_ExchangeId",
                table: "TradingBot",
                column: "ExchangeId",
                principalTable: "Exchanges",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
