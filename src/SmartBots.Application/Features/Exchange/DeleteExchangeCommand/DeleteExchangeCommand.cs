using MediatR;

namespace SmartBots.Application.Features.Exchange
{
    public record DeleteExchangeCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}