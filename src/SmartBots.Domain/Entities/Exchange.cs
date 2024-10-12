using SmartBots.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace SmartBots.Data.Models
{
    public class Exchange : BaseAuditableEntity
    {
        public string UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime DatePosted { get; set; } = DateTime.Now; // Default value using C# initializer

        [Required]
        public ExchangeType Type { get; set; }

        [Required]
        public string ApiKey { get; set; }

        [Required]
        public string ApiSecret { get; set; }

        [Required]
        public bool IsTest { get; set; }

        public ICollection<TradingBot> TradingBots { get; set; } // One-to-Many relationship with TradingBot
    }

    public enum ExchangeType
    {
        Binance,
        Coinbase
    }
}
