namespace SmartBots.Domain.Entities.IndicatorRules
{
    public class MACDRule : IndicatorRule
    {
        public int FastPeriod { get; set; } = 12;
        public int SlowPeriod { get; set; } = 26;
        public int SignalLinePeriod { get; set; } = 9;

        public override Signal EvaluateSignal()
        {
            throw new NotImplementedException();
        }
    }

}
