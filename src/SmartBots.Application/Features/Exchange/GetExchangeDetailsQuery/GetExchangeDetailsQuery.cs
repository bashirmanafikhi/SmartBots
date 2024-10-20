using MediatR;

namespace SmartBots.Application.Features.Exchange;

public record GetExchangeDetailsQuery : IRequest<ExchangeAccountDto>
{
    public Guid Id { get; set; }
}
