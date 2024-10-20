using Binance.Net;
using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Objects.Models.Spot;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
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
                Balances = data.Balances.Select(b => b.ToBalance())
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
                .Where(b => b.Total > 0)
                .Select(b => b.ToBalance());
        }

        public async Task<IEnumerable<Asset>> GetAssetsAsync()
        {
            var exchangeInfo = await GetExchangeInfoAsync();
            return exchangeInfo.Data.Symbols
                .SelectMany(s => new[] { s.BaseAsset, s.QuoteAsset })
                .Distinct()
                .Select(a => new Asset { Name = a });
        }

        public async Task<IEnumerable<Symbol>> GetAvailableSymbolsAsync(bool isSpotTradingAllowed = true)
        {
            var exchangeInfo = await GetExchangeInfoAsync();
            return exchangeInfo.Data.Symbols
                .Where(s => s.Status == SymbolStatus.Trading && s.IsSpotTradingAllowed == isSpotTradingAllowed)
                .Select(s => new Symbol
                {
                    Name = s.Name,
                    BaseAsset = s.BaseAsset,
                    QuoteAsset = s.QuoteAsset
                });
        }

        private async Task<WebCallResult<BinanceExchangeInfo>> GetExchangeInfoAsync()
        {
            var exchangeInfo = await _client.SpotApi.ExchangeData.GetExchangeInfoAsync();
            if (!exchangeInfo.Success || exchangeInfo.Data == null)
            {
                throw new Exception($"Failed to fetch exchange account information: {exchangeInfo.Error?.Message}");
            }
            return exchangeInfo;
        }

        public async Task<Order> PlaceOrderAsync(OrderRequest orderRequest)
        {
            var orderResponse = await _client.SpotApi.Trading.PlaceOrderAsync(
                symbol: orderRequest.Symbol,
                side: orderRequest.Side.ToBinanceOrderSide(),
                type: orderRequest.Type.ToBinanceSpotOrderType(),
                quantity: orderRequest.Quantity,
                timeInForce: orderRequest.TimeInForce.ToBinanceTimeInForce(),
                price: orderRequest.Price
            );

            if (!orderResponse.Success)
            {
                throw new Exception($"Failed to place order: {orderResponse.Error?.Message}");
            }

            return orderResponse.Data.ToOrder();
        }

        public async Task CancelOrderAsync(string symbol, long orderId)
        {
            var cancelResponse = await _client.SpotApi.Trading.CancelOrderAsync(symbol, orderId);
            if (!cancelResponse.Success)
            {
                throw new Exception($"Failed to cancel order: {cancelResponse.Error?.Message}");
            }
        }

        public async Task<IEnumerable<Order>> GetOpenOrdersAsync(string symbol = null)
        {
            var openOrders = await _client.SpotApi.Trading.GetOpenOrdersAsync(symbol);
            if (!openOrders.Success)
            {
                throw new Exception($"Failed to retrieve open orders: {openOrders.Error?.Message}");
            }

            return openOrders.Data.Select(o => o.ToOrder());
        }

        public async Task<Order> GetOrderAsync(string symbol, long orderId)
        {
            var orderDetails = await _client.SpotApi.Trading.GetOrderAsync(symbol, orderId);
            if (!orderDetails.Success)
            {
                throw new Exception($"Failed to retrieve order details: {orderDetails.Error?.Message}");
            }

            return orderDetails.Data.ToOrder();
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(string symbol = null, DateTime? startTime = null, DateTime? endTime = null)
        {
            var orderHistory = await _client.SpotApi.Trading.GetOrdersAsync(symbol, startTime: startTime, endTime: endTime);
            if (!orderHistory.Success)
            {
                throw new Exception($"Failed to retrieve order history: {orderHistory.Error?.Message}");
            }

            return orderHistory.Data.Select(o => o.ToOrder());
        }

        public async Task<bool> TestOrderAsync(OrderRequest orderRequest)
        {
            var orderResponse = await _client.SpotApi.Trading.PlaceTestOrderAsync(
                symbol: orderRequest.Symbol,
                side: orderRequest.Side.ToBinanceOrderSide(),
                type: orderRequest.Type.ToBinanceSpotOrderType(),
                quantity: orderRequest.Quantity,
                timeInForce: orderRequest.TimeInForce.ToBinanceTimeInForce(),
                price: orderRequest.Price
            );

            if (!orderResponse.Success)
            {
                throw new Exception($"Failed to place test order: {orderResponse.Error?.Message}");
            }

            return true;
        }
    }
}