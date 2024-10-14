using MediatR;

namespace SmartBots.Application.Features.Exchange;

public record GetExchangeDetailsQuery : IRequest<ExchangeDto>
{
    public Guid Id { get; set; }
}
