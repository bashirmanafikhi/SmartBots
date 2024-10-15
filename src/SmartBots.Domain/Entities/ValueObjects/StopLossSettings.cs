namespace SmartBots.Domain.Entities
{
    public class StopLossSettings
    {
        public bool UseStopLoss { get; set; } = false;
        public double StopLossPercentage { get; set; } = 5.0;
        public bool IncludeExtraOrdersPositions { get; set; } = false;
        public bool TrailingStopLoss { get; set; } = false;
        public int TimeoutSeconds { get; set; } = 60;
    }

}
