using SmartBots.Application.Interfaces;
using SmartBots.BinancePlatform;
using SmartBots.Domain.Entities;
using SmartBots.Domain.Enums;

namespace SmartBots.Infrastructure.Common
{
    internal class ExchangeFactory : IExchangeFactory
    {
        public IExchangeClient CreateExchangeClient(Exchange exchange)
        {
            switch (exchange.Type)
            {
                case ExchangeType.Binance:
                    return new BinanceClient(exchange.ApiKey, exchange.ApiSecret);
                default:
                    throw new ArgumentException("Invalid exchange type");
            }
        }

        public IMarketDataClient CreateMarketDataClient(Exchange exchange)
        {
            switch (exchange.Type)
            {
                case ExchangeType.Binance:
                    return new BinanceMarketDataClient(exchange.ApiKey, exchange.ApiSecret);
                default:
                    throw new ArgumentException("Invalid exchange type");
            }
        }

        public IExchangeWebSocketClient CreateWebSocketClient(ExchangeType exchangeType)
        {
            switch (exchangeType)
            {
                case ExchangeType.Binance:
                    return new BinanceWebSocketClient();
                default:
                    throw new ArgumentException("Invalid exchange type");
            }
        }
    }
}
