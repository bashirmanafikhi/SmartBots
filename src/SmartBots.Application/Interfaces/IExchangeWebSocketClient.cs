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
        Task<bool> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<KlineUpdateData> onUpdate, CancellationToken cancellationToken);
        Task UnsubscribeAllAsync();
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
}
