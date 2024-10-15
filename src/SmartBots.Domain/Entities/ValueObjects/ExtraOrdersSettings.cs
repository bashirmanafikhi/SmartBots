namespace SmartBots.Domain.Entities
{
    public class ExtraOrdersSettings
    {
        public int Count { get; set; } = 0;

        public double FirstVolumeScale { get; set; } = 1;

        public double FirstDeviationPercentage { get; set; } = 1;

        public double StepVolumeScale { get; set; } = 1;

        public double StepDeviationScale { get; set; } = 1;
    }

}
