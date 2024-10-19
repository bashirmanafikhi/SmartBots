using Binance.Net.Clients;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.Options;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBots.BinancePlatform
{
    internal class SocketTest
    {
        public async Task ConnectToSocket()
        {
            // Replace with your actual API credentials (not recommended for public use)
            var apiKey = "YOUR_API_KEY";
            var apiSecret = "YOUR_API_SECRET";

            // Create BinanceSocketClient with optional configuration
            var socketClient = new BinanceSocketClient(options =>
            {
                options.ApiCredentials = new ApiCredentials(apiKey, apiSecret);

                // Additional  options like AutoReconnect and LogVerbosity can be set here
            });

            // Subscribe to ETH/USDT ticker updates
            var tickerSubscriptionResult = await socketClient.SpotApi.ExchangeData
                .SubscribeToTickerUpdatesAsync("ETHUSDT", (update) =>
                {
                    var lastPrice = update.Data.LastPrice;
                    Console.WriteLine($"ETH/USDT Last Price: {lastPrice}");
                });

            if (tickerSubscriptionResult.Success)
            {
                Console.WriteLine("Successfully subscribed to ETH/USDT ticker updates.");

                // Keep the application running to receive updates (loop or wait for user input)
                Console.WriteLine("Press any key to unsubscribe and exit...");
                Console.ReadKey();

                await socketClient.UnsubscribeAllAsync();
            }
            else
            {
                Console.WriteLine($"Failed to subscribe: {tickerSubscriptionResult.Error}");
            }
        }
    }
}
