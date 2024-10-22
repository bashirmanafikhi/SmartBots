namespace SmartBots.Domain.Entities.IndicatorRules
{
    public class BollingerBandsRule : IndicatorRule
    {
        public int Period { get; set; } = 20;
        public double StandardDeviation { get; set; } = 2.0;

    }

}
