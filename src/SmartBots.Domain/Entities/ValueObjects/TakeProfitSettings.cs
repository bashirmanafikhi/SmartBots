namespace SmartBots.Domain.Entities
{
    public class TakeProfitSettings
    {
        public bool UseTakeProfit { get; set; } = false;

        public double TakeProfitPercentage { get; set; } = 10;

        public bool IncludeExtraOrdersPositions { get; set; } = false;

        public bool TrailingTakeProfit { get; set; } = false;

        public double TrailingDeviationPercentage { get; set; } = 3;
    }

}
