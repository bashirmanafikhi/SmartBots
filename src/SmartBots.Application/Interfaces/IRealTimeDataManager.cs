﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBots.Application.Interfaces
{
    public interface IRealTimeDataManager
    {
        Task SubscribeToKlineAndTickerUpdates(string symbol, KlineInterval interval,
            Action<List<Kline>, decimal> onUpdate, CancellationToken cancellationToken = default);
        List<Kline> GetCandlestickData();
        decimal GetLastPrice();
    }
}