namespace SmartBots.Application.Interfaces
{
    public interface IExchangeClient
    {
        #region Account Information
        Task<ExchangeAccountInfo> GetAccountInfoAsync();
        #endregion

        #region Assets
        Task<IEnumerable<Balance>> GetBalancesAsync();
        Task<IEnumerable<Asset>> GetAssetsAsync();
        #endregion

        #region Spot Trading
        //Task<Order> PlaceOrderAsync(OrderRequest orderRequest);
        //Task<Order> CancelOrderAsync(string orderId);
        //Task<IEnumerable<Order>> GetOpenOrdersAsync();
        //Task<Order> GetOrderByIdAsync(string orderId);
        //Task<IEnumerable<Trade>> GetOrderHistoryAsync(string symbol = null, int limit = 100);
        #endregion

        #region Market Data (Optional) - Consider adding for future implementations
        // You can add methods for retrieving market data like:
        // - GetTicker(string symbol)
        // - GetOrderBook(string symbol)
        // - GetRecentTrades(string symbol)
        #endregion
    }

    public class ExchangeAccountInfo
    {
        public decimal MakerCommission { get; set; }
        public decimal TakerCommission { get; set; }
        public decimal BuyerCommission { get; set; }
        public decimal SellerCommission { get; set; }
        public bool CanTrade { get; set; }
        public bool CanWithdraw { get; set; }
        public bool CanDeposit { get; set; }
        public IEnumerable<Balance> Balances { get; set; }
    }

    public class Balance
    {
        public string Asset { get; set; }
        public decimal Free { get; set; }
        public decimal Locked { get; set; }
        public decimal Total { get; set; }
    }

    //public class OrderRequest
    //{
    //    public string Symbol { get; set; }
    //    public OrderSide Side { get; set; }
    //    public OrderType Type { get; set; }
    //    public decimal TimeInForce { get; set; }
    //    public decimal Quantity { get; set; }
    //    public decimal Price { get; set; }
    //}

    //public class Order
    //{
    //    public string Symbol { get; set; }
    //    public OrderSide Side { get; set; }
    //    public OrderType Type { get; set; }
    //    public decimal TimeInForce { get; set; }
    //    public decimal Quantity { get; set; }
    //    public decimal Price { get; set; }
    //    public decimal Filled { get; set; }
    //    public decimal TotalFilled { get; set; }
    //    public decimal AveragePrice { get; set; }
    //    public string Status { get; set; }
    //    public DateTime Time { get; set; }
    //}

    //public class Trade
    //{
    //    public long Id { get; set; }
    //    public string Price { get; set; }
    //    public string Quantity { get; set; }
    //    public long QuoteQty { get; set; }
    //    public OrderSide Side { get; set; }
    //    public DateTime Time { get; set; }
    //    public string Symbol { get; set; }
    //}

    //public class Ticker
    //{
    //    public string Symbol { get; set; }
    //    public decimal PriceChange { get; set; }
    //    public decimal PriceChangePercent { get; set; }
    //    public decimal WeightedAvgPrice
    //    {
    //        get; set;
    //    }
    //    public decimal PrevClosePrice { get; set; }
    //    public decimal LastPrice { get; set; }
    //    public decimal
    // OpenPrice
    //    { get; set; }
    //    public decimal HighPrice { get; set; }
    //    public decimal LowPrice { get; set; }
    //    public decimal Volume { get; set; }
    //    public decimal QuoteVolume { get; set; }
    //    public
    // int OpenTime
    //    { get; set; }
    //    public int CloseTime { get; set; }
    //    public int FirstId { get; set; }
    //    public int LastId { get; set; }
    //    public int Count { get; set; }
    //}

    public class Asset
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; } // For calculating fees or other purposes
        public bool IsMarginEnabled { get; set; }
        public bool IsFuturesEnabled { get; set; }

        // Additional properties as needed:
        // - MinimumOrderQty
        // - PricePrecision
        // - QuantityPrecision
        // - BaseAsset
        // - QuoteAsset
        // ...
    }

    //public enum OrderSide
    //{
    //    Buy,
    //    Sell
    //}

    //public enum OrderType
    //{
    //    Limit,
    //    Market,
    //    StopLoss,
    //    StopLimit,
    //    TakeProfit,
    //    TakeProfitLimit,
    //    LimitMaker
    //}
}
