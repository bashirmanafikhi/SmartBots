using MediatR;
using Microsoft.Extensions.Logging;
using SmartBots.Application.Features.ExchangeApi.PlaceOrderCommand;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Entities;
using System.Collections.Concurrent;

namespace SmartBots.Infrastructure.Services
{
    public class TradingBotManager : ITradingBotManager
    {
        private readonly List<Order> orders = new List<Order>();

        private readonly ConcurrentDictionary<Guid, TradingBot> _activeBots = new();
        private readonly IMediator _mediator;
        private readonly IRealTimeDataManager _realTimeDataManager;
        private readonly ITradingRuleManager _tradingRuleManager;
        private readonly ILogger<TradingBotManager> _logger;

        public TradingBotManager(IMediator mediator, IRealTimeDataManager realTimeDataManager, ITradingRuleManager tradingRuleManager, ILogger<TradingBotManager> logger)
        {
            _mediator = mediator;
            _realTimeDataManager = realTimeDataManager ?? throw new ArgumentNullException(nameof(realTimeDataManager));
            _tradingRuleManager = tradingRuleManager;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Starts a trading bot asynchronously. Prevents duplicate starts.
        /// </summary>
        public async Task StartBotAsync(TradingBot bot, KlineInterval interval, CancellationToken cancellationToken)
        {
            if (bot == null) throw new ArgumentNullException(nameof(bot));

            if (_activeBots.ContainsKey(bot.Id))
            {
                _logger.LogWarning("Bot {BotId} is already running.", bot.Id);
                return; // Prevent duplicate start
            }

            if (!_activeBots.TryAdd(bot.Id, bot))
            {
                _logger.LogError("Failed to add bot {BotName} with ID {BotId} to active list.", bot.Name, bot.Id);
                return;
            }

            try
            {
                _logger.LogInformation("Starting bot {BotName} with ID {BotId}.", bot.Name, bot.Id);
                // Subscribe to real-time data updates
                string symbol = $"{bot.BaseAsset}{bot.QuoteAsset}";

                await _realTimeDataManager.SubscribeToAll(
                    bot.ExchangeAccountId,
                    symbol,
                    interval,
                    async (klineData, lastPrice) =>
                    {
                        try
                        {
                            if (klineData.Count == 0)
                                return;

                            var signal = await _tradingRuleManager.EvaluateSignalsAsync(klineData, bot.TradingRules);

                            await HandleSignal(bot, lastPrice, symbol, signal);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, "Error while handling kline");
                        }
                    },
                    orderUpdateData =>
                    {
                        _logger.LogInformation("orderUpdateData = " + orderUpdateData.Status);
                    },
                    cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to start bot {BotName} with ID {BotId}.", bot.Name, bot.Id);
                _activeBots.TryRemove(bot.Id, out _); // Rollback if startup fails
            }
        }

        private async Task HandleSignal(TradingBot bot, decimal lastPrice, string symbol, TradingSignal signal)
        {
            switch (signal)
            {
                case TradingSignal.Buy:
                    await HandleBuySignal(bot, lastPrice, symbol);
                    break;

                case TradingSignal.Sell:
                    await HandleSellSignal(bot, lastPrice, symbol);
                    break;

                case TradingSignal.Hold:
                    //_logger.LogInformation("Hold signal for bot {BotName} at {Time}.", bot.Name, DateTime.UtcNow);
                    break;
            }
        }

        private async Task HandleSellSignal(TradingBot bot, decimal lastPrice, string symbol)
        {
            if (orders.Count == 0 || orders.LastOrDefault()?.Side == OrderSide.BUY)
            {
                var order = await _mediator.Send(new PlaceOrderCommand(bot.ExchangeAccountId, new OrderRequest
                {
                    Price = lastPrice,
                    Quantity = (decimal)bot.TradeSize,
                    Side = OrderSide.SELL,
                    Symbol = symbol,
                    TimeInForce = TimeInForce.FOK,
                    Type = OrderType.LIMIT
                }));
                orders.Add(order);
                _logger.LogInformation("Sell order placed for bot {BotName} at {Time}.", bot.Name, DateTime.UtcNow);
                _logger.LogInformation($"Order Status: {order.Status}");
            }
        }

        private async Task HandleBuySignal(TradingBot bot, decimal lastPrice, string symbol)
        {
            if (orders.Count == 0 || orders.LastOrDefault()?.Side == OrderSide.SELL)
            {
                var order = await _mediator.Send(new PlaceOrderCommand(bot.ExchangeAccountId, new OrderRequest
                {
                    Price = lastPrice,
                    Quantity = (decimal)bot.TradeSize,
                    Side = OrderSide.BUY,
                    Symbol = symbol,
                    TimeInForce = TimeInForce.FOK,
                    Type = OrderType.LIMIT
                }));
                orders.Add(order);
                _logger.LogInformation("Buy order placed for bot {BotName} at {Time}.", bot.Name, DateTime.UtcNow);
                _logger.LogInformation($"Order Status: {order.Status}");
            }
        }

        /// <summary>
        /// Stops a bot and removes it from the active list.
        /// </summary>
        public void StopBot(Guid botId)
        {
            if (_activeBots.TryRemove(botId, out var bot))
            {
                _logger.LogInformation("Bot {BotName} with ID {BotId} stopped.", bot?.Name, botId);
            }
            else
            {
                _logger.LogWarning("Attempted to stop bot {BotId}, but it was not found.", botId);
            }
        }

        /// <summary>
        /// Gets the list of active bots.
        /// </summary>
        public IEnumerable<TradingBot> GetActiveBots() => _activeBots.Values;

        /// <summary>
        /// Checks if a bot is currently running.
        /// </summary>
        public bool IsBotRunning(Guid botId) => _activeBots.ContainsKey(botId);
    }
}
