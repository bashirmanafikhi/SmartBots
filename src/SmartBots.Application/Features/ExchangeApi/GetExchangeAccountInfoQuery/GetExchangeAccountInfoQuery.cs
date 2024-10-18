using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi
{
    public class GetExchangeAccountInfoQuery : IRequest<ExchangeAccountInfo>
    {
        public Guid ExchangeId { get; }

        public GetExchangeAccountInfoQuery(Guid exchangeId)
        {
            ExchangeId = exchangeId;
        }
    }
}
