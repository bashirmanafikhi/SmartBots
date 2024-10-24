using Binance.Net;
using Binance.Net.Clients;
using CryptoExchange.Net.Authentication;
using SmartBots.Application.Interfaces;

namespace SmartBots.BinancePlatform
{
    public class BinanceWebSocketClient : IExchangeWebSocketClient
    {
        private readonly BinanceSocketClient _socketClient;

        public BinanceWebSocketClient()
        {
            _socketClient = new BinanceSocketClient();
        }

        public BinanceWebSocketClient(string apiKey, string apiSecret)
        {
            _socketClient = new BinanceSocketClient(options =>
            {
                options.ApiCredentials = new ApiCredentials(apiKey, apiSecret);
                options.Environment = BinanceEnvironment.Testnet;
            });
        }

        public async Task<bool> SubscribeToTickerUpdatesAsync(string symbol, Action<TickerData> onUpdate, CancellationToken cancellationToken)
        {
            var subscription = await _socketClient.SpotApi.ExchangeData.SubscribeToTickerUpdatesAsync(
                symbol, update => onUpdate(update.Data.ToTickerData()), cancellationToken);

            if (subscription.Success)
            {
                Console.WriteLine($"Successfully subscribed to {symbol} ticker updates.");
                return true;
            }
            else
            {
                Console.WriteLine($"Failed to subscribe: {subscription.Error}");
                return false;
            }
        }

        public async Task<bool> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<KlineUpdateData> onUpdate, CancellationToken cancellationToken)
        {
            var subscription = await _socketClient.SpotApi.ExchangeData.SubscribeToKlineUpdatesAsync(
                symbol, interval.ToBinanceKlineInterval(), update => onUpdate(update.Data.ToKlineData()), cancellationToken);

            if (subscription.Success)
            {
                Console.WriteLine($"Successfully subscribed to {symbol} Kline updates.");
                return true;
            }
            else
            {
                Console.WriteLine($"Failed to subscribe: {subscription.Error}");
                return false;
            }
        }

        public async Task<bool> SubscribeToUserDataUpdatesAsync(
        Action<OrderUpdateData> onOrderUpdate,
        Action<OcoOrderUpdateData> onOcoOrderUpdate,
        Action<AccountPositionUpdateData> onAccountPositionUpdate,
        Action<BalanceUpdateData> onBalanceUpdate,
        Action onListenKeyExpired,
        CancellationToken cancellationToken)
        {
            var listenKeyResult = await _socketClient.SpotApi.Account.StartUserStreamAsync();
            if (!listenKeyResult.Success)
            {
                Console.WriteLine($"Failed to start user stream: {listenKeyResult.Error}");
                return false;
            }

            var listenKey = listenKeyResult.Data.Result;

            var subscription = await _socketClient.SpotApi.Account.SubscribeToUserDataUpdatesAsync(
                listenKey,
                orderUpdate =>
                {
                    if (onOrderUpdate is not null)
                        onOrderUpdate(orderUpdate.Data.ToOrderUpdateData());
                },
                ocoUpdate =>
                {
                    if (onOcoOrderUpdate is not null)
                        onOcoOrderUpdate(ocoUpdate.Data.ToOcoOrderUpdateData());
                },
                accountPosition =>
                {
                    if (onAccountPositionUpdate is not null)
                        onAccountPositionUpdate(accountPosition.Data.ToAccountPositionUpdateData());
                },
                balanceUpdate =>
                {
                    if (onBalanceUpdate is not null)
                        onBalanceUpdate(balanceUpdate.Data.ToBalanceUpdateData());
                },
                listenKeyExpired =>
                {
                    if (onListenKeyExpired is not null)
                        onListenKeyExpired();
                },
                cancellationToken);

            if (subscription.Success)
            {
                Console.WriteLine("Successfully subscribed to user data updates.");
                return true;
            }
            else
            {
                Console.WriteLine($"Failed to subscribe: {subscription.Error}");
                return false;
            }
        }

        public async Task UnsubscribeAllAsync()
        {
            await _socketClient.UnsubscribeAllAsync();
        }

        public async Task KeepListenKeyAliveAsync(string listenKey, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(30), cancellationToken);  // Ping every 30 minutes

                var keepAliveResult = await _socketClient.SpotApi.Account.KeepAliveUserStreamAsync(listenKey);
                if (!keepAliveResult.Success)
                {
                    Console.WriteLine($"Failed to keep listen key alive: {keepAliveResult.Error}");
                    // Handle reconnection logic if needed (e.g., request a new listen key)
                }
            }
        }

    }
}
