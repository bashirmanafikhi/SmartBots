using SmartBots.Domain.Entities;
using SmartBots.Domain.Enums;

namespace SmartBots.Application.Interfaces
{
    public interface IExchangeAccountFactory
    {
        IExchangeClient CreateExchangeClient(ExchangeAccount exchange);
        IMarketDataClient CreateMarketDataClient(ExchangeAccount exchange);
        IExchangeWebSocketClient CreateWebSocketClient(ExchangeType exchangeType);
    }
}
