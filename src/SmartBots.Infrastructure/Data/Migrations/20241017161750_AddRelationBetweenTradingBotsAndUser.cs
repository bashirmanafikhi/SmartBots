using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBots.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenTradingBotsAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "TradingBot",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TradingBot_ApplicationUserId",
                table: "TradingBot",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TradingBot_AspNetUsers_ApplicationUserId",
                table: "TradingBot",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TradingBot_AspNetUsers_ApplicationUserId",
                table: "TradingBot");

            migrationBuilder.DropIndex(
                name: "IX_TradingBot_ApplicationUserId",
                table: "TradingBot");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "TradingBot",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
