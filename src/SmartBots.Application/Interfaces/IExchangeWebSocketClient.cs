namespace SmartBots.Application.Interfaces
{
    public interface IExchangeWebSocketClient
    {
        Task<bool> SubscribeToTickerUpdatesAsync(string symbol, Action<TickerData> onUpdate, CancellationToken cancellationToken);
        Task<bool> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<KlineUpdateData> onUpdate, CancellationToken cancellationToken);

        Task UnsubscribeAllAsync();
        Task KeepListenKeyAliveAsync(string listenKey, CancellationToken cancellationToken);
        Task<bool> SubscribeToUserDataUpdatesAsync(Action<OrderUpdateData> onOrderUpdate, Action<OcoOrderUpdateData> onOcoOrderUpdate, Action<AccountPositionUpdateData> onAccountPositionUpdate, Action<BalanceUpdateData> onBalanceUpdate, Action onListenKeyExpired, CancellationToken cancellationToken);
    }

    public class TickerData
    {
        public string Symbol { get; set; }
        public decimal LastPrice { get; set; }
    }

    public class KlineUpdateData : Kline
    {
        public string Symbol { get; set; }
        public KlineInterval Interval { get; set; }

    }

    public class OrderUpdateData
    {
        public string Symbol { get; set; }
        public long OrderId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public OrderStatus Status { get; set; }
    }
    public class OcoOrderUpdateData
    {
        public string Symbol { get; set; }
        public string ListId { get; set; }
        public string Status { get; set; }
    }
    public class AccountPositionUpdateData
    {
        public List<PositionData> Positions { get; set; }
    }

    public class PositionData
    {
        public string Asset { get; set; }
        public decimal Free { get; set; }
        public decimal Locked { get; set; }
    }

    public class BalanceUpdateData
    {
        public string Asset { get; set; }
        public decimal Balance { get; set; }
    }


}
