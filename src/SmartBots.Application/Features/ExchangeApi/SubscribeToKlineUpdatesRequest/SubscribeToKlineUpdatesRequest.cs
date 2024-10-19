using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBots.Application.Features.ExchangeApi.SubscribeToKlineUpdatesRequest
{
    public class SubscribeToKlineUpdatesRequest : IRequest<bool>
    {
        public ExchangeType ExchangeType { get; set; } = ExchangeType.Binance;
        public string Symbol { get; set; }
        public KlineInterval Interval { get; set; }
        public Action<KlineData> OnUpdate { get; set; }
    }
}
