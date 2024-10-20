using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.ExchangeApi
{
    public class GetExchangeAccountInfoQuery : IRequest<ExchangeAccountInfo>
    {
        public Guid ExchangeAccountId { get; }

        public GetExchangeAccountInfoQuery(Guid exchangeAccountId)
        {
            ExchangeAccountId = exchangeAccountId;
        }
    }
}
