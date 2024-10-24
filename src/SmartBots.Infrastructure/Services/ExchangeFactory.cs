﻿using SmartBots.Application.Interfaces;
using SmartBots.BinancePlatform;
using SmartBots.Domain.Entities;
using SmartBots.Domain.Enums;

namespace SmartBots.Infrastructure.Services
{
    internal class ExchangeFactory : IExchangeFactory
    {
        public IExchangeClient CreateExchangeClient(ExchangeAccount exchange)
        {
            switch (exchange.Type)
            {
                case ExchangeType.Binance:
                    return new BinanceClient(exchange.ApiKey, exchange.ApiSecret);
                default:
                    throw new ArgumentException("Invalid exchange account type");
            }
        }

        public IExchangeClient CreateExchangeClient(ExchangeType exchangeType)
        {
            switch (exchangeType)
            {
                case ExchangeType.Binance:
                    return new BinanceClient();
                default:
                    throw new ArgumentException("Invalid exchange account type");
            }
        }

        public IMarketDataClient CreateMarketDataClient(ExchangeAccount exchange)
        {
            switch (exchange.Type)
            {
                case ExchangeType.Binance:
                    return new BinanceMarketDataClient(exchange.ApiKey, exchange.ApiSecret);
                default:
                    throw new ArgumentException("Invalid exchange account type");
            }
        }

        public IMarketDataClient CreateMarketDataClient(ExchangeType exchangeType)
        {
            switch (exchangeType)
            {
                case ExchangeType.Binance:
                    return new BinanceMarketDataClient();
                default:
                    throw new ArgumentException("Invalid exchange account type");
            }
        }

        public IExchangeWebSocketClient CreateWebSocketClient(ExchangeAccount exchange)
        {
            switch (exchange.Type)
            {
                case ExchangeType.Binance:
                    return new BinanceWebSocketClient(exchange.ApiKey, exchange.ApiSecret);
                default:
                    throw new ArgumentException("Invalid exchange account type");
            }
        }

        public IExchangeWebSocketClient CreateWebSocketClient(ExchangeType exchangeType)
        {
            switch (exchangeType)
            {
                case ExchangeType.Binance:
                    return new BinanceWebSocketClient();
                default:
                    throw new ArgumentException("Invalid exchange account type");
            }
        }
    }
}
