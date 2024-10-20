using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBots.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditingToTradingRule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "TradingRule",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TradingRule",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedBy",
                table: "TradingRule",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "TradingRule",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TradingRule");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TradingRule");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "TradingRule");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "TradingRule");
        }
    }
}
