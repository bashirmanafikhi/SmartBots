using MediatR;
using SmartBots.Application.Features.ExchangeApi.GetKlinesQuery;
using SmartBots.Application.Features.ExchangeApi.SubscribeToKlineUpdatesRequest;
using SmartBots.Application.Features.ExchangeApi.SubscribeToTickerUpdatesRequest;
using SmartBots.Application.Interfaces;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;


namespace SmartBots.Infrastructure.Services
{
    public class RealTimeDataManager : IRealTimeDataManager
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RealTimeDataManager> _logger;
        private readonly ConcurrentBag<Kline> _candlestickData = new(); // Thread-safe storage

        public decimal LastPrice { get; private set; }

        public RealTimeDataManager(IMediator mediator, ILogger<RealTimeDataManager> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public List<Kline> GetCandlestickData() => _candlestickData.ToList();
        public decimal GetLastPrice() => LastPrice;

        public async Task SubscribeToKlineAndTickerUpdates(
            string symbol,
            KlineInterval interval,
            Action<List<Kline>, decimal> onUpdate,
            CancellationToken cancellationToken = default)
        {
            try
            {
                await SubscribeToTickerUpdates(symbol, onUpdate, cancellationToken);
                await LoadCandlestickDataAndSubscribeAsync(symbol, interval, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error subscribing to Kline and Ticker updates for {Symbol}", symbol);
            }
        }

        private async Task SubscribeToTickerUpdates(
            string symbol,
            Action<List<Kline>, decimal> onUpdate,
            CancellationToken cancellationToken)
        {
            try
            {
                bool isConnected = await _mediator.Send(new SubscribeToTickerUpdatesRequest
                {
                    Symbol = symbol,
                    OnUpdate = tickerData =>
                    {
                        LastPrice = tickerData.LastPrice;
                        _logger.LogInformation("Received Ticker Update: LastPrice = {LastPrice}", LastPrice);
                        onUpdate?.Invoke(GetCandlestickData(), LastPrice);
                    }
                }, cancellationToken);

                if (!isConnected)
                    _logger.LogWarning("Failed to subscribe to ticker updates for {Symbol}", symbol);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error subscribing to ticker updates for {Symbol}", symbol);
            }
        }

        private async Task LoadCandlestickDataAndSubscribeAsync(
            string symbol,
            KlineInterval interval,
            CancellationToken cancellationToken)
        {
            try
            {
                DateTime endTime = DateTime.UtcNow;
                DateTime startTime = endTime.AddHours(-1); // Example: Last hour of data

                var historicalData = await GetHistoricalKlinesAsync(symbol, interval, startTime, endTime, cancellationToken);
                foreach (var kline in historicalData)
                    _candlestickData.Add(kline);

                _logger.LogInformation("Loaded {Count} historical Klines for {Symbol}.", historicalData.Count(), symbol);

                bool isSubscribed = await SubscribeToKlineUpdatesAsync(symbol, interval, OnKlineUpdate, cancellationToken);
                if (isSubscribed)
                    _logger.LogInformation("Subscribed to real-time Kline updates for {Symbol}.", symbol);
                else
                    _logger.LogWarning("Failed to subscribe to Kline updates for {Symbol}.", symbol);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading historical data and subscribing for {Symbol}.", symbol);
            }
        }

        private async Task<IEnumerable<Kline>> GetHistoricalKlinesAsync(
            string symbol,
            KlineInterval interval,
            DateTime startTime,
            DateTime endTime,
            CancellationToken cancellationToken)
        {
            try
            {
                return await _mediator.Send(new GetKlinesQuery(
                    ExchangeAccountId: Guid.Parse("e13a8bc2-f9f7-4157-a2e2-08dceec2868f"),
                    Symbol: symbol,
                    Interval: interval,
                    StartTime: startTime,
                    EndTime: endTime), cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching historical Klines for {Symbol}.", symbol);
                return Enumerable.Empty<Kline>();
            }
        }

        private void OnKlineUpdate(KlineUpdateData klineData)
        {
            try
            {
                var existingCandle = _candlestickData.FirstOrDefault(c => c.OpenTime == klineData.OpenTime);

                if (existingCandle != null)
                {
                    existingCandle.ClosePrice = klineData.ClosePrice;
                    existingCandle.HighPrice = Math.Max(existingCandle.HighPrice, klineData.HighPrice);
                    existingCandle.LowPrice = Math.Min(existingCandle.LowPrice, klineData.LowPrice);
                    existingCandle.Volume = klineData.Volume;

                    _logger.LogInformation("Updated existing candle: {OpenTime}", existingCandle.OpenTime);
                }
                else
                {
                    var newCandle = new Kline
                    {
                        OpenTime = klineData.OpenTime,
                        OpenPrice = klineData.OpenPrice,
                        HighPrice = klineData.HighPrice,
                        LowPrice = klineData.LowPrice,
                        ClosePrice = klineData.ClosePrice,
                        Volume = klineData.Volume,
                        CloseTime = klineData.CloseTime
                    };
                    _candlestickData.Add(newCandle);

                    _logger.LogInformation("Added new candle: {OpenTime} - {CloseTime}", newCandle.OpenTime, newCandle.CloseTime);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing Kline update.");
            }
        }

        private async Task<bool> SubscribeToKlineUpdatesAsync(
            string symbol,
            KlineInterval interval,
            Action<KlineUpdateData> onUpdate,
            CancellationToken cancellationToken)
        {
            try
            {
                return await _mediator.Send(new SubscribeToKlineUpdatesRequest
                {
                    Symbol = symbol,
                    Interval = interval,
                    OnUpdate = onUpdate
                }, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error subscribing to Kline updates for {Symbol}.", symbol);
                return false;
            }
        }
    }

}
