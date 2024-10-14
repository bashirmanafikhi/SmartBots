using SmartBots.Application.Interfaces;
using SmartBots.BinancePlatform;
using SmartBots.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                //case ExchangeType.Coinbase:
                //    return new CoinbaseClient();
                // Add more cases for other exchanges
                default:
                    throw new ArgumentException("Invalid exchange type");
            }
        }
    }
}
