using SmartBots.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBots.Application.Interfaces
{
    public interface ITradingRuleManager
    {
        Task<TradingSignal> EvaluateSignalsAsync(List<Kline> klines, List<TradingRule> tradingRules);
    }
    public enum TradingSignal
    {
        Hold,  // No action
        Buy,   // Open a position
        Sell   // Close a position
    }
}
