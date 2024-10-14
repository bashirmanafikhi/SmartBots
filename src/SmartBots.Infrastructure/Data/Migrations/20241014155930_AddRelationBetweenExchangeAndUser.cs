using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBots.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenExchangeAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Exchanges",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Exchanges_ApplicationUserId",
                table: "Exchanges",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exchanges_AspNetUsers_ApplicationUserId",
                table: "Exchanges",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exchanges_AspNetUsers_ApplicationUserId",
                table: "Exchanges");

            migrationBuilder.DropIndex(
                name: "IX_Exchanges_ApplicationUserId",
                table: "Exchanges");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Exchanges");

        }
    }
}
