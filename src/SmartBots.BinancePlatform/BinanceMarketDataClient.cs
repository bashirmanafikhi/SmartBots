using Binance.Net;
using Binance.Net.Clients;
using CryptoExchange.Net.Authentication;
using SmartBots.Application.Interfaces;

namespace SmartBots.BinancePlatform
{
    public class BinanceMarketDataClient : IMarketDataClient
    {
        private readonly BinanceRestClient _client;

        public BinanceMarketDataClient()
        {
            _client = new BinanceRestClient();
        }

        public BinanceMarketDataClient(string apiKey, string secretKey)
        {
            _client = new BinanceRestClient(options =>
            {
                options.ApiCredentials = new ApiCredentials(apiKey, secretKey);
                options.Environment = BinanceEnvironment.Testnet;
            });
        }

        public async Task<IEnumerable<Kline>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null)
        {
            var response = await _client.SpotApi.ExchangeData.GetKlinesAsync(symbol, interval.ToBinanceKlineInterval(), startTime, endTime);
            if (!response.Success)
                throw new Exception($"Failed to retrieve klines: {response.Error?.Message}");

            return response.Data.Select(k => k.ToKline());
        }

        public async Task<TickerPrice> GetTickerPriceAsync(string symbol)
        {
            var response = await _client.SpotApi.ExchangeData.GetPriceAsync(symbol);
            if (!response.Success)
                throw new Exception($"Failed to retrieve ticker price: {response.Error?.Message}");

            return response.Data.ToTickerPrice();
        }

        public async Task<OrderBook> GetOrderBookAsync(string symbol, int limit = 100)
        {
            var response = await _client.SpotApi.ExchangeData.GetOrderBookAsync(symbol, limit);
            if (!response.Success)
                throw new Exception($"Failed to retrieve order book: {response.Error?.Message}");

            return new OrderBook
            {
                Bids = response.Data.Bids.Select(x => x.ToOrderBookEntry()).ToList(),
                Asks = response.Data.Asks.Select(x => x.ToOrderBookEntry()).ToList()
            };
        }

        public async Task<IEnumerable<TickerPrice>> GetAllTickerPricesAsync()
        {
            var response = await _client.SpotApi.ExchangeData.GetPricesAsync();
            if (!response.Success)
                throw new Exception($"Failed to retrieve all ticker prices: {response.Error?.Message}");

            return response.Data.Select(p => p.ToTickerPrice());
        }
    }

}
