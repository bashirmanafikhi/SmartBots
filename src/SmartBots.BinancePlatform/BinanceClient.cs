using Binance.Net;
using Binance.Net.Clients;
using CryptoExchange.Net.Authentication;
using SmartBots.Application.Interfaces;


namespace SmartBots.BinancePlatform
{
    public class BinanceClient : IExchangeClient
    {
        private readonly BinanceRestClient _client;

        public BinanceClient(string apiKey, string secretKey)
        {

            // get Bitcoin price data using Binance
            var client = new Binance.Net.Clients.BinanceRestClient();
            var klines = client.SpotApi.ExchangeData.GetKlinesAsync(
                symbol: "BTCUSDT",
                interval: Binance.Net.Enums.KlineInterval.OneMinute,
                startTime: DateTime.Now.AddDays(-2),
                endTime: DateTime.Now,
                limit: 50);


            _client = new BinanceRestClient(options =>
            {
                options.ApiCredentials = new ApiCredentials(apiKey, secretKey);
                options.Environment = BinanceEnvironment.Testnet;
            });
        }

        //public Task<Order> CancelOrderAsync(string orderId)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<AccountInfo> GetAccountInfoAsync()
        //{
        //    throw new NotImplementedException();

        //    var response = await _client.SpotApi.Account.GetAccountInfoAsync();

        //    return new AccountInfo
        //    {

        //    };
        //}

        //public Task<IEnumerable<Asset>> GetAssetsAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<IEnumerable<Balance>> GetBalancesAsync()
        //{
        //    throw new NotImplementedException();

        //    var response = await _client.SpotApi.Account.GetBalancesAsync();

        //    return new List<Balance>();
        //}

        //public Task<IEnumerable<Order>> GetOpenOrdersAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Order> GetOrderByIdAsync(string orderId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<Trade>> GetOrderHistoryAsync(string symbol = null, int limit = 100)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Order> PlaceOrderAsync(OrderRequest orderRequest)
        //{
        //    throw new NotImplementedException();
        //    //var binanceOrder = new Order
        //    //{
        //    //    Symbol = orderRequest.Symbol,
        //    //    Side = (OrderSide)orderRequest.Side,
        //    //    Type = (OrderType)orderRequest.Type,
        //    //    Quantity = orderRequest.Quantity,
        //    //    Price = orderRequest.Price
        //    //};

        //    //var response = await _client.SpotApi.Order.CreateOrderAsync(binanceOrder);
        //    //// Map response data to Order object (implementation omitted for brevity)
        //    //return order;
        //}
    }
}
