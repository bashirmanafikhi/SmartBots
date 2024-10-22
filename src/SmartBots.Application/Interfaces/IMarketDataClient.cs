namespace SmartBots.Application.Interfaces
{
    public interface IMarketDataClient
    {
        Task<IEnumerable<Kline>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null);
        Task<TickerPrice> GetTickerPriceAsync(string symbol);
        Task<OrderBook> GetOrderBookAsync(string symbol, int limit = 100);
        Task<IEnumerable<TickerPrice>> GetAllTickerPricesAsync();  // All available tickers
    }

    public enum KlineInterval
    {
        OneSecond = 1,
        OneMinute = 60,
        ThreeMinutes = 60 * 3,
        FiveMinutes = 60 * 5,
        FifteenMinutes = 60 * 15,
        ThirtyMinutes = 60 * 30,
        OneHour = 60 * 60,
        TwoHour = 60 * 60 * 2,
        FourHour = 60 * 60 * 4,
        SixHour = 60 * 60 * 6,
        EightHour = 60 * 60 * 8,
        TwelveHour = 60 * 60 * 12,
        OneDay = 60 * 60 * 24,
        ThreeDay = 60 * 60 * 24 * 3,
        OneWeek = 60 * 60 * 24 * 7,
        OneMonth = 60 * 60 * 24 * 30
    }

    public class Kline
    {
        public DateTime OpenTime { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal Volume { get; set; }
        public DateTime CloseTime { get; set; }
    }


    public class TickerPrice
    {
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public DateTime? Timestamp { get; set; }
    }

    public class OrderBook
    {
        public List<OrderBookEntry> Bids { get; set; } = new List<OrderBookEntry>();
        public List<OrderBookEntry> Asks { get; set; } = new List<OrderBookEntry>();
    }

    public class OrderBookEntry
    {
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
    }

}
