using SmartBots.Application.Interfaces;
using SmartBots.Domain.Entities.IndicatorRules;
using SmartBots.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBots.Infrastructure.Services
{
    public class TradingRuleManager : ITradingRuleManager
    {
        private readonly ITechnicalAnalysisService _technicalAnalysisService;

        public TradingRuleManager(ITechnicalAnalysisService technicalAnalysisService)
        {
            _technicalAnalysisService = technicalAnalysisService;
        }

        public async Task<TradingSignal> EvaluateSignalsAsync(List<Kline> klines, List<TradingRule> tradingRules)
        {
            var buySignals = new List<bool>();
            var sellSignals = new List<bool>();

            foreach (var rule in tradingRules)
            {
                switch (rule)
                {
                    case BollingerBandsRule bollingerRule:
                        var (_, bands) = _technicalAnalysisService.CalculateBollingerBands(klines, bollingerRule);
                        bool buyBollinger = klines.Last().ClosePrice < (decimal)bands.Last().Lower;
                        bool sellBollinger = klines.Last().ClosePrice > (decimal)bands.Last().Upper;

                        buySignals.Add(rule.IsUsedForOpening && buyBollinger);
                        sellSignals.Add(rule.IsUsedForClosing && sellBollinger);
                        break;

                    case MACDRule macdRule:
                        var (_, macdData) = _technicalAnalysisService.CalculateMACD(klines, macdRule);
                        var latestMacd = macdData.Last();
                        bool buyMacd = latestMacd.MACD > latestMacd.Signal;
                        bool sellMacd = latestMacd.MACD < latestMacd.Signal;

                        buySignals.Add(rule.IsUsedForOpening && buyMacd);
                        sellSignals.Add(rule.IsUsedForClosing && sellMacd);
                        break;

                    case RSIRule rsiRule:
                        var (_, rsiValues) = _technicalAnalysisService.CalculateRSI(klines, rsiRule);
                        double latestRsi = rsiValues.Last();
                        bool buyRsi = latestRsi < rsiRule.OversoldThreshold;
                        bool sellRsi = latestRsi > rsiRule.OverboughtThreshold;

                        buySignals.Add(rule.IsUsedForOpening && buyRsi);
                        sellSignals.Add(rule.IsUsedForClosing && sellRsi);
                        break;
                }
            }

            // Determine the final signal
            // we can do a lot here,
            // for example to ensure all the trading rules gave the same signal?
            // or one of them is enough? 
            // or we can make more complexe scenarios

            // for now, I prefer to ensure all the rules gave the same signal 

            if (buySignals.Count == tradingRules.Count)
                return TradingSignal.Buy;

            if (sellSignals.Count == tradingRules.Count)
                return TradingSignal.Sell;

            return TradingSignal.Hold;
        }
    }
}
