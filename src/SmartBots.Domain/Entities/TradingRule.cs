using SmartBots.Domain.Common;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Domain.Entities
{
    public abstract class TradingRule : BaseAuditableEntity
    {
        // Foreign Key and Navigation Property
        public Guid TradingBotId { get; set; }
        public virtual TradingBot TradingBot { get; set; }

        // Usage Flags
        public bool IsUsedForOpening { get; set; }
        public bool IsUsedForClosing { get; set; }

        // Abstract Method to be implemented by derived classes
        public abstract Signal EvaluateSignal();
    }

}
