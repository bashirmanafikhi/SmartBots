using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBots.Application.Interfaces
{
    public interface IExchangeWebSocketClient
    {
        Task<bool> SubscribeToTickerUpdatesAsync(string symbol, Action<TickerData> onUpdate, CancellationToken cancellationToken);
        Task<bool> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<KlineData> onUpdate, CancellationToken cancellationToken);
        Task UnsubscribeAllAsync();
    }

    public class TickerData
    {
        public string Symbol { get; set; }
        public decimal LastPrice { get; set; }
    }

    public class KlineData
    {
        public string Symbol { get; set; }
        public KlineInterval Interval { get; set; }

        /// <summary>
        /// The time this candlestick opened
        /// </summary>
        public DateTime OpenTime { get; set; }

        /// <summary>
        /// The price at which this candlestick opened
        /// </summary>
        public decimal OpenPrice { get; set; }

        /// <summary>
        /// The highest price in this candlestick
        /// </summary>
        public decimal HighPrice { get; set; }

        /// <summary>
        /// The lowest price in this candlestick
        /// </summary>
        public decimal LowPrice { get; set; }

        /// <summary>
        /// The price at which this candlestick closed
        /// </summary>
        public decimal ClosePrice { get; set; }

        /// <summary>
        /// The volume traded during this candlestick
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// The close time of this candlestick
        /// </summary>
        public DateTime CloseTime { get; set; }
    }
}
