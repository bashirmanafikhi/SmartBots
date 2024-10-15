namespace SmartBots.Domain.Entities.IndicatorRules
{
    public class RSIRule : IndicatorRule
    {
        public int Period { get; set; } = 14;
        public double OverboughtThreshold { get; set; } = 70.0;
        public double OversoldThreshold { get; set; } = 30.0;

        public override Signal EvaluateSignal()
        {
            throw new NotImplementedException();
        }
    }

}
