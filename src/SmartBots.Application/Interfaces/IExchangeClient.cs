﻿using SmartBots.Domain.Enums;

namespace SmartBots.Application.Interfaces
{
    public interface IExchangeClient
    {
        // Account and balance information
        Task<ExchangeAccountInfo> GetAccountInfoAsync();
        Task<IEnumerable<Balance>> GetBalancesAsync();
        Task<IEnumerable<Asset>> GetAssetsAsync();
        Task<IEnumerable<Symbol>> GetAvailableSymbolsAsync(bool isSpotTradingAllowed = true);

        // Order operations
        Task<Order> PlaceOrderAsync(OrderRequest orderRequest);
        Task CancelOrderAsync(string symbol, long orderId);
        Task<IEnumerable<Order>> GetOpenOrdersAsync(string symbol = null);
        Task<IEnumerable<Order>> GetOrdersAsync(string symbol = null, DateTime? startTime = null, DateTime? endTime = null);
        Task<Order> GetOrderAsync(string symbol, long orderId);
        Task<bool> TestOrderAsync(OrderRequest orderRequest);
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

    public class Symbol
    {
        public string Name { get; set; }
        public string BaseAsset { get; set; }
        public string QuoteAsset { get; set; }
    }

    public class OrderRequest
    {
        public string Symbol { get; set; }
        public OrderSide Side { get; set; }
        public OrderType Type { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public TimeInForce TimeInForce { get; set; }
    }

    public class Order
    {
        public long Id { get; set; }
        public string Symbol { get; set; }
        public OrderSide Side { get; set; }
        public OrderType Type { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Filled { get; set; }
        public TimeInForce TimeInForce { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public class Asset
    {
        public string Name { get; set; }
    }

    public enum OrderSide
    {
        BUY,
        SELL
    }
    public enum OrderStatus
    {
        NEW,
        PARTIALLY_FILLED,
        FILLED,
        CANCELED,
        PENDING_CANCEL,
        REJECTED,
        EXPIRED
    }
    public enum OrderType
    {
        LIMIT,
        MARKET,
        STOP
    }

    public enum TimeInForce
    {
        /// <summary>
        /// GoodTillCanceled orders will stay active until they are filled or canceled
        /// </summary>
        GTC,

        /// <summary>
        /// ImmediateOrCancel orders have to be at least partially filled upon placing or will be automatically canceled
        /// </summary>
        IOC,

        /// <summary>
        /// FillOrKill orders have to be entirely filled upon placing or will be automatically canceled
        /// </summary>
        FOK,

        /// <summary>
        /// GoodTillCrossing orders will post only
        /// </summary>
        GTX,

        /// <summary>
        /// Good til the order expires or is canceled
        /// </summary>
        GTE_GTC,

        /// <summary>
        /// Good til date
        /// </summary>
        GTD
    }
}
