using SmartBots.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBots.Application.Interfaces
{
    public interface ITradingBotManager
    {
        Task StartBotAsync(TradingBot bot, KlineInterval interval, CancellationToken cancellationToken);
        void StopBot(Guid botId);
        IEnumerable<TradingBot> GetActiveBots();
        bool IsBotRunning(Guid botId);
    }
}
