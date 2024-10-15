using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartBots.Domain.Entities;
using SmartBots.Domain.Entities.IndicatorRules;

namespace SmartBots.Infrastructure.Data.Configurations
{
    public class TradingRuleConfiguration : IEntityTypeConfiguration<TradingRule>
    {
        public void Configure(EntityTypeBuilder<TradingRule> builder)
        {
            // Table-per-hierarchy (TPH) for TradingRules
            builder.HasDiscriminator<string>("RuleType")
                   .HasValue<BollingerBandsRule>("BollingerBands")
                   .HasValue<RSIRule>("RSI")
                   .HasValue<MACDRule>("MACD");
        }
    }
}
