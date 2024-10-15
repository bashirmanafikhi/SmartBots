using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBots.Migrations
{
    /// <inheritdoc />
    public partial class AddTradingBotAndTradingRulesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TradingBot",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExchangeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaseAsset = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    QuoteAsset = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TradeSize = table.Column<double>(type: "float", nullable: false),
                    BotType = table.Column<int>(type: "int", nullable: false),
                    ExtraOrders_Count = table.Column<int>(type: "int", nullable: false),
                    ExtraOrders_FirstVolumeScale = table.Column<double>(type: "float", nullable: false),
                    ExtraOrders_FirstDeviationPercentage = table.Column<double>(type: "float", nullable: false),
                    ExtraOrders_StepVolumeScale = table.Column<double>(type: "float", nullable: false),
                    ExtraOrders_StepDeviationScale = table.Column<double>(type: "float", nullable: false),
                    StopLoss_UseStopLoss = table.Column<bool>(type: "bit", nullable: false),
                    StopLoss_StopLossPercentage = table.Column<double>(type: "float", nullable: false),
                    StopLoss_IncludeExtraOrdersPositions = table.Column<bool>(type: "bit", nullable: false),
                    StopLoss_TrailingStopLoss = table.Column<bool>(type: "bit", nullable: false),
                    StopLoss_TimeoutSeconds = table.Column<int>(type: "int", nullable: false),
                    TakeProfit_UseTakeProfit = table.Column<bool>(type: "bit", nullable: false),
                    TakeProfit_TakeProfitPercentage = table.Column<double>(type: "float", nullable: false),
                    TakeProfit_IncludeExtraOrdersPositions = table.Column<bool>(type: "bit", nullable: false),
                    TakeProfit_TrailingTakeProfit = table.Column<bool>(type: "bit", nullable: false),
                    TakeProfit_TrailingDeviationPercentage = table.Column<double>(type: "float", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradingBot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradingBot_Exchanges_ExchangeId",
                        column: x => x.ExchangeId,
                        principalTable: "Exchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TradingRule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TradingBotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsUsedForOpening = table.Column<bool>(type: "bit", nullable: false),
                    IsUsedForClosing = table.Column<bool>(type: "bit", nullable: false),
                    RuleType = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    BollingerBandsRule_Period = table.Column<int>(type: "int", nullable: true),
                    StandardDeviation = table.Column<double>(type: "float", nullable: true),
                    FastPeriod = table.Column<int>(type: "int", nullable: true),
                    SlowPeriod = table.Column<int>(type: "int", nullable: true),
                    SignalLinePeriod = table.Column<int>(type: "int", nullable: true),
                    Period = table.Column<int>(type: "int", nullable: true),
                    OverboughtThreshold = table.Column<double>(type: "float", nullable: true),
                    OversoldThreshold = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradingRule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TradingRule_TradingBot_TradingBotId",
                        column: x => x.TradingBotId,
                        principalTable: "TradingBot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TradingBot_ExchangeId",
                table: "TradingBot",
                column: "ExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_TradingRule_TradingBotId",
                table: "TradingRule",
                column: "TradingBotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TradingRule");

            migrationBuilder.DropTable(
                name: "TradingBot");
        }
    }
}
