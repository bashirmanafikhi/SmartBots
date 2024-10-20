using MediatR;

namespace SmartBots.Application.Features.Exchange
{
    public record GetAllExchangeAccountsQuery : IRequest<List<ExchangeAccountDto>>;
}
