using Binance.Net;
using Binance.Net.Clients;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Authentication;
using SmartBots.Application.Interfaces;


namespace SmartBots.BinancePlatform
{
    public class BinanceClient : IExchangeClient
    {
        private readonly BinanceRestClient _client;

        public BinanceClient(string apiKey, string secretKey)
        {
            _client = new BinanceRestClient(options =>
            {
                options.ApiCredentials = new ApiCredentials(apiKey, secretKey);
                options.Environment = BinanceEnvironment.Testnet;
            });
        }

        public async Task<ExchangeAccountInfo> GetAccountInfoAsync()
        {
            var accountInfoResponse = await _client.SpotApi.Account.GetAccountInfoAsync();

            if (!accountInfoResponse.Success)
            {
                throw new Exception($"Failed to retrieve account info: {accountInfoResponse.Error?.Message}");
            }

            var data = accountInfoResponse.Data;

            return new ExchangeAccountInfo
            {
                MakerCommission = data.MakerFee,
                TakerCommission = data.TakerFee,
                BuyerCommission = data.BuyerFee,
                SellerCommission = data.SellerFee,
                CanTrade = data.CanTrade,
                CanWithdraw = data.CanWithdraw,
                CanDeposit = data.CanDeposit,
                Balances = data.Balances.Select(b => ConverToBalance(b))
            };
        }

        public async Task<IEnumerable<Balance>> GetBalancesAsync()
        {
            var accountInfo = await _client.SpotApi.Account.GetAccountInfoAsync();

            if (!accountInfo.Success)
            {
                throw new Exception($"Failed to fetch balances: {accountInfo.Error?.Message}");
            }

            return accountInfo.Data.Balances
                .Where(b => b.Total > 0) // Filter only balances with non-zero total
                .Select(b => ConverToBalance(b));
        }

        private Balance ConverToBalance(BinanceBalance b)
        {
            return new Balance
            {
                Asset = b.Asset,
                Free = b.Available,
                Locked = b.Locked,
                Total = b.Total
            };
        }

        public async Task<IEnumerable<Asset>> GetAssetsAsync()
        {
            var exchangeInfo = await _client.SpotApi.ExchangeData.GetExchangeInfoAsync();

            if (!exchangeInfo.Success)
            {
                throw new Exception($"Failed to fetch assets: {exchangeInfo.Error?.Message}");
            }

            return exchangeInfo.Data.Symbols
                .SelectMany(s => new[] { s.BaseAsset, s.QuoteAsset })
                .Distinct() // Remove duplicate asset names
                .Select(a => new Asset { Name = a });
        }
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