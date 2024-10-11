using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartBots.Data.Models
{
    public class TradingBot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string UserId { get; set; }

        public int ExchangeId { get; set; }

        [ForeignKey("ExchangeId")]
        public Exchange Exchange { get; set; } // Navigation property for Exchange relationship

        [Required]
        public DateTime DatePosted { get; set; } = DateTime.UtcNow; // Default value using C# initializer

        [Required]
        [StringLength(255)] // Limit string length
        public string Name { get; set; }

        [Required]
        [StringLength(100)] // Limit string length
        public string BaseAsset { get; set; }

        [Required]
        [StringLength(100)] // Limit string length
        public string QuoteAsset { get; set; }

        public float? TradeSize { get; set; } // Nullable float

        public BotType BotType { get; set; } = BotType.LONG; // Default value

        // Stop Loss
        [Required]
        public bool UseStopLoss { get; set; } = false;

        [Required]
        public float StopLossPercentage { get; set; } = 5;

        [Required]
        public bool StopLossIncludeExtraOrdersPositions { get; set; } = false;

        [Required]
        public bool TrailingStopLoss { get; set; } = false;

        [Required]
        public int StopLossTimeout { get; set; } = 60; // Time in minutes (adjust if needed)

        // Take Profit
        [Required]
        public bool UseTakeProfit { get; set; } = false;

        [Required]
        public float TakeProfitPercentage { get; set; } = 10;

        [Required]
        public bool TakeProfitIncludeExtraOrdersPositions { get; set; } = false;

        [Required]
        public bool TrailingTakeProfit { get; set; } = false;

        [Required]
        public float TrailingTakeProfitDeviationPercentage { get; set; } = 3;

        // Extra Orders
        [Required]
        public int ExtraOrdersCount { get; set; } = 0;

        [Required]
        public float ExtraOrderFirstVolumeScale { get; set; } = 1;

        [Required]
        public float ExtraOrderFirstDeviationPercentage { get; set; } = 1;

        [Required]
        public float ExtraOrderStepVolumeScale { get; set; } = 1;

        [Required]
        public float ExtraOrderStepDeviationScale { get; set; } = 1;

        // Start Conditions
        public string StartConditions { get; set; } // String representation of JSON (adjust as needed)
    }

    public enum BotType
    {
        LONG,
        SHORT, // Add other bot types if needed
    }
}
