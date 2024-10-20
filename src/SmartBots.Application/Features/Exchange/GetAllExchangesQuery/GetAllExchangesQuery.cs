using MediatR;

namespace SmartBots.Application.Features.Exchange
{
    public record GetAllExchangesQuery : IRequest<List<ExchangeAccountDto>>;
}
