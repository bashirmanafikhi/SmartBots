using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartBots.Domain.Entities;
using System.Reflection.Emit;

namespace SmartBots.Infrastructure.Data.Configurations
{
    public class TradingBotConfiguration : IEntityTypeConfiguration<TradingBot>
    {
        public void Configure(EntityTypeBuilder<TradingBot> builder)
        {
            // Owned entity types
            builder.OwnsOne(tb => tb.ExtraOrders);
            builder.OwnsOne(tb => tb.StopLoss);
            builder.OwnsOne(tb => tb.TakeProfit);

            // Table-per-hierarchy (TPH) for TradingRules
            builder.HasMany(tb => tb.TradingRules)
                   .WithOne(tr => tr.TradingBot)
                   .HasForeignKey(tr => tr.TradingBotId);

            builder.HasOne<ApplicationUser>()
                .WithMany(au => au.TradingBots)
                .HasForeignKey(tb => tb.ApplicationUserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
