using SmartBots.Application.Interfaces;
using SmartBots.Domain.Entities.IndicatorRules;
using TALib;
using static TALib.Core;

namespace SmartBots.Infrastructure.Services
{
    public class TechnicalAnalysisService : ITechnicalAnalysisService
    {
        public (List<Kline> klines, List<(double Upper, double Middle, double Lower)> bands)
            CalculateBollingerBands(List<Kline> klines, BollingerBandsRule rule)
        {
            var closePrices = klines.Select(k => (double)k.ClosePrice).ToArray();
            double[] upperBand = new double[closePrices.Length];
            double[] middleBand = new double[closePrices.Length];
            double[] lowerBand = new double[closePrices.Length];
            RetCode retCode = Core.Bbands(
                closePrices, 0, closePrices.Length - 1,
                upperBand, middleBand, lowerBand,
                out int outBegIdx, out int outNbElement,
                MAType.Sma, rule.Period, rule.StandardDeviation, rule.StandardDeviation);

            if (retCode != RetCode.Success)
                throw new Exception("Failed to calculate Bollinger Bands.");

            var bands = ExtractResultBands(upperBand, middleBand, lowerBand, outBegIdx, outNbElement);
            return (klines.Skip(outBegIdx).Take(outNbElement).ToList(), bands);
        }

        public (List<Kline> klines, List<(double MACD, double Signal, double Hist)> macdData)
            CalculateMACD(List<Kline> klines, MACDRule rule)
        {
            var closePrices = klines.Select(k => (double)k.ClosePrice).ToArray();
            double[] macd = new double[closePrices.Length];
            double[] macdSignal = new double[closePrices.Length];
            double[] macdHist = new double[closePrices.Length];
            RetCode retCode = Core.Macd(
                closePrices, 0, closePrices.Length - 1,
                macd, macdSignal, macdHist,
                out int outBegIdx, out int outNbElement,
                rule.FastPeriod, rule.SlowPeriod, rule.SignalLinePeriod);

            if (retCode != RetCode.Success)
                throw new Exception("Failed to calculate MACD.");

            var macdData = ExtractMACDResult(macd, macdSignal, macdHist, outBegIdx, outNbElement);
            return (klines.Skip(outBegIdx).Take(outNbElement).ToList(), macdData);
        }

        public (List<Kline> klines, List<double> rsiValues)
            CalculateRSI(List<Kline> klines, RSIRule rule)
        {
            var closePrices = klines.Select(k => (double)k.ClosePrice).ToArray();
            double[] rsi = new double[closePrices.Length];
            RetCode retCode = Core.Rsi(
                closePrices, 0, closePrices.Length - 1,
                rsi, out int outBegIdx, out int outNbElement,
                rule.Period);

            if (retCode != RetCode.Success)
                throw new Exception("Failed to calculate RSI.");

            var rsiValues = rsi.Skip(outBegIdx).Take(outNbElement).ToList();
            return (klines.Skip(outBegIdx).Take(outNbElement).ToList(), rsiValues);
        }

        private List<(double Upper, double Middle, double Lower)> ExtractResultBands(
            double[] upperBand, double[] middleBand, double[] lowerBand, int outBegIdx, int outNbElement)
        {
            var bands = new List<(double Upper, double Middle, double Lower)>();
            for (int i = 0; i < outNbElement; i++)
            {
                bands.Add((upperBand[outBegIdx + i], middleBand[outBegIdx + i], lowerBand[outBegIdx + i]));
            }
            return bands;
        }

        private List<(double MACD, double Signal, double Hist)> ExtractMACDResult(
            double[] macd, double[] macdSignal, double[] macdHist, int outBegIdx, int outNbElement)
        {
            var macdData = new List<(double MACD, double Signal, double Hist)>();
            for (int i = 0; i < outNbElement; i++)
            {
                macdData.Add((macd[outBegIdx + i], macdSignal[outBegIdx + i], macdHist[outBegIdx + i]));
            }
            return macdData;
        }
    }
}

