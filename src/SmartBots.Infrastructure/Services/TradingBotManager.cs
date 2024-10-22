using Microsoft.Extensions.Logging;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Entities;
using System.Collections.Concurrent;

namespace SmartBots.Infrastructure.Services
{
    public class TradingBotManager : ITradingBotManager
    {
        private readonly ConcurrentDictionary<Guid, TradingBot> _activeBots = new();
        private readonly IRealTimeDataManager _realTimeDataManager;
        private readonly ITradingRuleManager _tradingRuleManager;
        private readonly ILogger<TradingBotManager> _logger;

        public TradingBotManager(IRealTimeDataManager realTimeDataManager, ITradingRuleManager tradingRuleManager, ILogger<TradingBotManager> logger)
        {
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

            if (_activeBots.TryAdd(bot.Id, bot))
            {
                _logger.LogInformation("Starting bot {BotName} with ID {BotId}.", bot.Name, bot.Id);

                try
                {
                    // Subscribe to real-time data updates
                    string symbol = $"{bot.BaseAsset}{bot.QuoteAsset}";

                    await _realTimeDataManager.SubscribeToKlineAndTickerUpdates(
                        symbol,
                        interval,
                        async (klineData, lastPrice) =>
                        {
                            if (klineData.Count == 0)
                                return;

                            var signal = await _tradingRuleManager.EvaluateSignalsAsync(klineData, bot.TradingRules);

                            switch (signal)
                            {
                                case TradingSignal.Buy:
                                    _logger.LogInformation("Buy signal detected for bot {BotName} at {Time}.", bot.Name, DateTime.UtcNow);
                                    // Place Buy Order Logic
                                    break;

                                case TradingSignal.Sell:
                                    _logger.LogInformation("Sell signal detected for bot {BotName} at {Time}.", bot.Name, DateTime.UtcNow);
                                    // Place Sell Order Logic
                                    break;

                                case TradingSignal.Hold:
                                    _logger.LogInformation("Hold signal for bot {BotName} at {Time}.", bot.Name, DateTime.UtcNow);
                                    break;
                            }

                            _logger.LogInformation("Evaluated signals for bot {BotName} at {Time}.", bot.Name, DateTime.UtcNow);
                        },
                        cancellationToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to start bot {BotName} with ID {BotId}.", bot.Name, bot.Id);
                    _activeBots.TryRemove(bot.Id, out _); // Rollback if startup fails
                }
            }
            else
            {
                _logger.LogError("Failed to add bot {BotName} with ID {BotId} to active list.", bot.Name, bot.Id);
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
