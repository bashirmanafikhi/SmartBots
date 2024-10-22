using SmartBots.Domain.Entities.IndicatorRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBots.Application.Interfaces
{
    public interface ITechnicalAnalysisService
    {
        (List<Kline> klines, List<(double Upper, double Middle, double Lower)> bands) CalculateBollingerBands(List<Kline> klines, BollingerBandsRule rule);
        (List<Kline> klines, List<(double MACD, double Signal, double Hist)> macdData) CalculateMACD(List<Kline> klines, MACDRule rule);
        (List<Kline> klines, List<double> rsiValues) CalculateRSI(List<Kline> klines, RSIRule rule);
    }
}
