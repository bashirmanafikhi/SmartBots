using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBots.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationBetweenTodoAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Todos",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_ApplicationUserId",
                table: "Todos",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_AspNetUsers_ApplicationUserId",
                table: "Todos",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_AspNetUsers_ApplicationUserId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_ApplicationUserId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Todos");
        }
    }
}
