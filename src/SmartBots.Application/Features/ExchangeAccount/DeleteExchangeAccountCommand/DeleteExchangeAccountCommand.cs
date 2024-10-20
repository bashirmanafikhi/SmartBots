using MediatR;

namespace SmartBots.Application.Features.Exchange
{
    public record DeleteExchangeAccountCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}