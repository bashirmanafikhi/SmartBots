using Binance.Net.Clients;
using Binance.Net.Interfaces;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Sockets;
using SmartBots.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBots.BinancePlatform
{
    public class BinanceWebSocketClient : IExchangeWebSocketClient
    {
        private readonly BinanceSocketClient _socketClient;

        public BinanceWebSocketClient()
        {
            _socketClient = new BinanceSocketClient();
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

        public async Task UnsubscribeAllAsync()
        {
            await _socketClient.UnsubscribeAllAsync();
        }
    }
}
