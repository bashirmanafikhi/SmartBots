using MediatR;

namespace SmartBots.Application.Features.Exchange;

public record GetExchangeAccountDetailsQuery : IRequest<ExchangeAccountDto>
{
    public Guid Id { get; set; }
}
