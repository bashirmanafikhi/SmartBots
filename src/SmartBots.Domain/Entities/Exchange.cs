using SmartBots.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace SmartBots.Domain.Entities
{
    public class Exchange : BaseAuditableEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public ExchangeType Type { get; set; }

        [Required]
        public string ApiKey { get; set; }

        [Required]
        public string ApiSecret { get; set; }

        [Required]
        public bool IsTest { get; set; }
    }

    public enum ExchangeType
    {
        Binance,
        Coinbase
    }
}
