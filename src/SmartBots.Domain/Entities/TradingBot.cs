using SmartBots.Domain.Common;
using SmartBots.Domain.Enums;
using SmartBots.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace SmartBots.Domain.Entities
{
    public class TradingBot : BaseAuditableEntity, IUserOwnedEntity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        public Guid ExchangeId { get; set; }
        public virtual Exchange Exchange { get; set; } // Navigation property

        [Required]
        [StringLength(100)]
        public string BaseAsset { get; set; }

        [Required]
        [StringLength(100)]
        public string QuoteAsset { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Trade size must be positive.")]
        public double TradeSize { get; set; }

        public BotType BotType { get; set; } = BotType.LONG;

        public virtual List<TradingRule> TradingRules { get; set; } = new List<TradingRule>();

        public ExtraOrdersSettings ExtraOrders { get; set; } = new ExtraOrdersSettings();
        public StopLossSettings StopLoss { get; set; } = new StopLossSettings();
        public TakeProfitSettings TakeProfit { get; set; } = new TakeProfitSettings();
    }
}
